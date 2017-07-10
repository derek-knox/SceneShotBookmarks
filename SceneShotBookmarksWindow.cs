// DOCUMENTATION -----------------------------------------------------------------------------------

// This custom EditorWindow exists to save and reset the Scene View camera to bookmarked settings.
// Scene Shot Bookmarks is useful for speeding up Scene View navigation and level design.
// Learn more at http://www.derekknox/articles/scene-shot-bookmarks-in-unity/

// DEPENDENCIES ------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

// DEFINITION --------------------------------------------------------------------------------------

public class SceneShotBookmarksWindow : EditorWindow
{

	// CACHE ---------------------------------------------------------------------------------------

	private const int BOOKMARK_BUTTON_HEIGHT = 22;
	private const string PATH_FOLDER = "Assets/Editor/SceneShotBookmarks";
	private const string PATH_FILE = "SceneShotBookmarks.asset";
	private const string PATH_FULL = PATH_FOLDER + "/" + PATH_FILE;

	private SceneShotBookmarksScriptableObject sceneShotBookmarksScriptableObject;
	private List<SceneShotBookmarkModel> sceneShotBookmarks;
	private SceneShotBookmarkModel currentBookmarkModel;

	private GUIStyle centeredLabelStyle;
	private GUIStyle centeredTextFieldStyle;

	// INITIALIZATION ------------------------------------------------------------------------------

	[MenuItem("Window/Scene Shot Bookmarks")]
	private static void Init()
	{
		SceneShotBookmarksWindow window = (SceneShotBookmarksWindow)EditorWindow.GetWindow(typeof(SceneShotBookmarksWindow), false, "Scene Shot Bookmarks");
		window.Show();
	}

	// HOOKS ---------------------------------------------------------------------------------------

	private void Awake()
	{
		initScriptableObject();
		AssetDatabase.Refresh();
	}

	private void OnGUI()
	{
		// UI Alignment
		alignUI();

		// Render UI
		renderRowStart();
		renderBookmarks();
		renderRowEnd();

		// Save changes
		if(GUI.changed)
		{
			EditorUtility.SetDirty(sceneShotBookmarksScriptableObject);
		}
	}

	// METHODS -------------------------------------------------------------------------------------

	private void initScriptableObject()
	{
		// Create asset if necessary and get asset reference
		if(!File.Exists(PATH_FULL))
		{
			sceneShotBookmarksScriptableObject = createSceneScriptableObject();
		}
		else
		{
			sceneShotBookmarksScriptableObject = (SceneShotBookmarksScriptableObject)AssetDatabase.LoadAssetAtPath(PATH_FULL, typeof(SceneShotBookmarksScriptableObject));
		}

		// Update sceneShotBookmarks for use in dynamically generating the UI
		sceneShotBookmarks = sceneShotBookmarksScriptableObject.sceneShotBookmarks;
	}

	private SceneShotBookmarksScriptableObject createSceneScriptableObject()
	{
		// Directory setup
		if(!Directory.Exists(PATH_FOLDER))
		{
			Directory.CreateDirectory(PATH_FOLDER);
		}

		// Create and serialize SceneShotBookmarksScriptableObject asset
		SceneShotBookmarksScriptableObject asset = ScriptableObject.CreateInstance<SceneShotBookmarksScriptableObject>();
		AssetDatabase.CreateAsset(asset, PATH_FULL);
		AssetDatabase.SaveAssets();

		// Return asset reference
		return asset;
	}

	private void saveBookmark(SceneShotBookmarkModel model)
	{
		// Update bookmark with Scene View camera data
		Camera sceneViewCamera = SceneView.lastActiveSceneView.camera;
		currentBookmarkModel = model;
		currentBookmarkModel.position = sceneViewCamera.transform.position;
		currentBookmarkModel.rotation =  sceneViewCamera.transform.rotation;
		currentBookmarkModel.orthographic =  sceneViewCamera.orthographic;
		currentBookmarkModel.orthographicSize = sceneViewCamera.orthographicSize;
	}

	private void setToBookmark(SceneShotBookmarkModel model)
	{
		// Update Scene View camera with bookmark data
		SceneView sceneView = SceneView.lastActiveSceneView;
		sceneView.pivot = model.position;
		sceneView.rotation = model.rotation;
		sceneView.orthographic = model.orthographic;
		sceneView.size = model.orthographicSize;
		SceneView.RepaintAll();
	}

	private void alignUI()
	{
		// Set Label and TextField styles
		centeredLabelStyle = new GUIStyle(GUI.skin.label);
		centeredLabelStyle.alignment = TextAnchor.UpperCenter;
		centeredTextFieldStyle = new GUIStyle(GUI.skin.textField);
		centeredTextFieldStyle.alignment = TextAnchor.UpperCenter;
	}

	private void renderRowStart()
	{
		// Start editor display
		EditorGUILayout.BeginHorizontal();
	}

	private void renderBookmarks()
	{
		// Escape condition for in-editor ScriptableObject edit of zero
		if(sceneShotBookmarks.Count == 0) { return; }

		// Valid count, so create bookmarks
		int count = sceneShotBookmarks.Count;
		int bookmarkWidth = Screen.width / count;
		SceneShotBookmarkModel model;
		for(int i = 0; i < count; i++)
		{
			model = sceneShotBookmarks[i];
			renderBookmarkStart(i * bookmarkWidth, bookmarkWidth);
			renderBookmarkTop(model, i + 1);
			renderBookmarkBottom(model);
			renderBookmarkEnd();
			if(i < count - 1)
				GUILayout.Space(10);
		}
	}

	private void renderBookmarkStart(int x, int width)
	{
		// Start bookmark display
		GUILayout.BeginArea(new Rect(x, 0, width, Screen.height - BOOKMARK_BUTTON_HEIGHT));
		EditorGUILayout.BeginVertical("Box", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(true));
	}

	private void renderBookmarkTop(SceneShotBookmarkModel model, int id)
	{
		// Set Scene View to bookmark setup
		Rect rect = EditorGUILayout.BeginVertical("Button", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
			GUILayout.FlexibleSpace();
				if(GUI.Button(rect, GUIContent.none))
					setToBookmark(model);
				GUILayout.Label((id) + "", centeredLabelStyle);
			GUILayout.FlexibleSpace();
		EditorGUILayout.EndVertical();

		// Bookmark nickname setup
		model.nickname = GUILayout.TextField(model.nickname, 50, centeredTextFieldStyle);
	}

	private void renderBookmarkBottom(SceneShotBookmarkModel model)
	{
		// Set bookmark setup
		Rect rect = EditorGUILayout.BeginHorizontal("Button", GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
			if(GUI.Button(rect, GUIContent.none))
				saveBookmark(model);
			GUILayout.Label("Bookmark", centeredLabelStyle);
		EditorGUILayout.EndHorizontal();
	}

	private void renderBookmarkEnd()
	{
		// End bookmark display
		EditorGUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void renderRowEnd()
	{
		// End editor display
		EditorGUILayout.EndHorizontal();
	}

	// HANDLERS ------------------------------------------------------------------------------------

	// No handlers
}