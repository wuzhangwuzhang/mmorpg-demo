﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour {
	public Text role_name;
	public Text role_level;

	void Awake() {
		Module.UI.main = this;
	}

	public void RefreshRole() {
		role_name.text = Module.Role.name;
		role_level.text = Module.Role.level.ToString();
	}

}
