# Scene Shot Bookmarks
Unity custom editor for bookmarking and resetting Scene View camera angles/settings in one-click

![Scene Shot Bookmarks Editor Window](http://derekknox.com/articles/scene-shot-bookmarks-in-unity/assets/img/scene-shot-bookmarks-custom-editor.png "Scene Shot Bookmarks Editor Window")

## Overview
Read more about the origin of Scene Shot Bookmarks at [derekknox.com/articles/scene-shot-bookmarks-in-unity/](http://derekknox.com/articles/scene-shot-bookmarks-in-unity/)

## Installation
1. Ensure the `Assets/Editor/SceneShotBookmarks` directory exists in your Unity project
2. Place all three files in the `SceneShotBookmarks` directory
    - `SceneShotBookmarkModel`
    - `SceneShotBookmarksScriptableObject`
    - `SceneShotBookmarksWindow`
3. In Unity, click the Window menu followed by clicking the `Scene Shot Bookmarks` option

## Usage
Once installed, you can:
1. Save the current Scene View camera state (Bookmark button)
2. Instantly reset the Scene View to a bookmarked camera angle (# button)

Additionally, you can:
- Share bookmarks with your team (via source control)
- Persist bookmarks after closing and reopening Unity (thanks ScriptableObject)
- Associate nicknames to bookmarks for context
- Change the bookmark count via the Inspector Window

![Scene Shot Bookmarks Editor Window Custom Count](http://derekknox.com/articles/scene-shot-bookmarks-in-unity/assets/img/scene-shot-bookmarks-custom-editor-count.png "Scene Shot Bookmarks Editor Window Custom Count")
