using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationController : MonoBehaviour {
    
	// ドロップメニュー
	public Dropdown dropdown_startTime;		// 開始時刻
	public Dropdown dropdown_endTime;		// 終了時刻
	public Dropdown dropdown_inputTime;		// 入力時間

	public Button btn_Check;				// 確認ボタン
	public Toggle toggle_Autoconfirm;		// 自動確認（オンにするときは結果を自動確認が行う）

	// 結果オブジェクト
	public GameObject obj_InRange;			// 範囲内	(IN RANGE!)
	public GameObject obj_OutOfRange;		// 範囲外	(OUT OF RANGE...)

	void Awake() {
		Input.multiTouchEnabled = false;

		btn_Check.onClick.AddListener(delegate {
			CheckTime();
		});

		toggle_Autoconfirm.onValueChanged.AddListener(delegate {
			if(toggle_Autoconfirm.isOn) {
				btn_Check.interactable = false;
 				ResetOuputs();
			} else {
				btn_Check.interactable = true;
				CheckTime();
			}
		});

		dropdown_startTime.onValueChanged.AddListener(delegate {
			if(toggle_Autoconfirm.isOn) {
				CheckTime();
			}
		});
		
		dropdown_endTime.onValueChanged.AddListener(delegate {
			if(toggle_Autoconfirm.isOn) {
				CheckTime();
			}
		});

		dropdown_inputTime.onValueChanged.AddListener(delegate {
			if(toggle_Autoconfirm.isOn) {
				CheckTime();
			}
		});
	}

	void CheckTime () {
		/* 
			Conditions:
				- No Count if Input Time = End Time, AND Start Time != End Time
				- Count as True if Start Time = End Time
				- If Start Time > End Time, swap values 
					(update dropdown_startTime & dropdown_endTime indicides to reflect changes)
				
			条件：
				- 入力時間は終了時間の場合カウントしない（開始時刻 ！＝ 終了時刻のみ）
				- 開始時刻と終了時刻が同じ場合カウントとなる
				- 開始時刻 ＞ 終了時刻の場合、数字を交換する
					（ドロップメニューにも反映させる）
		*/

		bool temp_output = false;
		int temp_startTimeIndex 	= dropdown_startTime.value;
		int temp_endTimeIndex 		= dropdown_endTime.value;
		int temp_inputTimeIndex 	= dropdown_inputTime.value;

		if(temp_startTimeIndex != temp_endTimeIndex) {

			// If Start Time > End Time
			// 開始時刻 ＞ 終了時刻の場合
			if(temp_startTimeIndex > temp_endTimeIndex) {
				// Swap values
				// 数字を交換する
				dropdown_endTime.value 		= temp_startTimeIndex;
				dropdown_startTime.value 	= temp_endTimeIndex;

				// Refresh temp values
				// 計算用数字を更新する
				temp_startTimeIndex = dropdown_startTime.value;
				temp_endTimeIndex 	= dropdown_endTime.value;
			}

			if((temp_inputTimeIndex >= temp_startTimeIndex) && (temp_inputTimeIndex < temp_endTimeIndex)) {
				temp_output = true;
			} else {
				temp_output = false;
			}
		} else {
			if(temp_inputTimeIndex == temp_startTimeIndex){
				temp_output = true;
			}
		}

		UpdateOuputs(temp_output);
	}

	// Update displayed ouputs
	// 結果表示を更新する
	public void UpdateOuputs(bool isInRange) {
		if(isInRange) {
			obj_InRange.SetActive(true);
			obj_OutOfRange.SetActive(false);
		} else {
			obj_InRange.SetActive(false);
			obj_OutOfRange.SetActive(true);
		}
	}

	// Reset displayed outputs (Hide All)
	// 結果表示をリセット（全部非表示）
	public void ResetOuputs() {
		obj_InRange.SetActive(false);
		obj_OutOfRange.SetActive(false);
	}
}