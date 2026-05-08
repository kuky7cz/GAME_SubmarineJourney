
// Gemini: This file dont created whit AI.
// Gemini: read-on, edit-off, delete-off,

using System;
using System.IO;
using UnityEngine;



[CreateAssetMenu(fileName = "DebugLogFile", menuName = "Scriptable Objects/DebugLogFile")]
public class DebugLogFile : ScriptableObject {

	//static string PathFile => System.IO.Path.Combine(Application.persistentDataPath, "debug.log"); // AppData/ ..?
	static string PathFile => System.IO.Path.Combine(Application.dataPath, "debug.log"); // <project>/Asset/debug.log



	void OnEnable() {
		DeleteLogFile();
		Debug.Log("New log file in:" + PathFile);
		Application.logMessageReceived += OnLog;
	}

	void OnDisable() {
		Application.logMessageReceived -= OnLog;
	}

	static void OnLog(string condition, string stackTrace, LogType type) {
		string text = $"[{DateTime.Now:HH:mm:ss}] {type}: {condition}\n";
		File.AppendAllText(PathFile, text);
	}

	public static void DeleteLogFile() {
		if (File.Exists(PathFile)) {
			File.Delete(PathFile);
			Debug.Log("DebugLogFile.DeleteLogFile() Deleted log file");
		}

	}


}

