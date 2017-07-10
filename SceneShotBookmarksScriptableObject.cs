using UnityEngine;
using System.Collections.Generic;

public class SceneShotBookmarksScriptableObject : ScriptableObject {
    
    private const int DEFAULT_BOOKMARK_COUNT = 3;
    
    public List<SceneShotBookmarkModel> sceneShotBookmarks;

    public SceneShotBookmarksScriptableObject()
    {
    	initModels();
    }

    private void initModels()
    {
    	// Prep sceneShotBookmarks creation
    	sceneShotBookmarks = new List<SceneShotBookmarkModel>();
    	SceneShotBookmarkModel model;
    	for(int i = 0; i < DEFAULT_BOOKMARK_COUNT; i++)
    	{
    		// Create and add model
    		model = new SceneShotBookmarkModel();
    		sceneShotBookmarks.Add(model);

    		// Update model
    		model.nickname = "nickname";
			model.position = new Vector3(0f, 0f, 0f);
			model.rotation = Quaternion.identity;
			model.orthographic = false;
			model.orthographicSize = 0;
    	}
    }
}