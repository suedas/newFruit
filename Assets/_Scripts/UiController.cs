using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
	#region Singleton
	public static UiController instance;
	void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}
	#endregion

	public GameObject winPanel, gamePanel, losePanel,tapToStartPanel;
	public TextMeshProUGUI scoreText,levelText,heartText;
	public int heart=2;
	public GameObject meyveler;
	private void Start()
	{
		gamePanel.SetActive(true);
		//tapToStartPanel.SetActive(true);
		//PlayerPrefs.DeleteAll();
		heart = 2;
		winPanel.SetActive(false);
		losePanel.SetActive(false);
		scoreText.text = PlayerPrefs.GetInt("score").ToString();
		heartText.text = 3.ToString();
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
	}

	public void NextLevelButtonClick()
	{
		winPanel.SetActive(false);
		//PlayerController.instance.PreStartingEvents();
		LevelController.instance.NextLevelEvents();
	}

	public void RestartButtonClick()
	{
		losePanel.SetActive(false);
		//tapToStartPanel.SetActive(true);
		//PlayerController.instance.PreStartingEvents();
		
		int meyve = meyveler.transform.childCount;
        for (int i = 0; i < meyve; i++)
        {
			
			Destroy(meyveler.transform.GetChild(i).gameObject);
        }
		heart = 2;
		heartText.text = (heart+1).ToString();
		LevelController.instance.RestartLevelEvents();
	}

	public void SetScoreText()
	{
		StartCoroutine(SetScoreTextAnim());
	}

	// bu fonksiyon sayesinde score textimiz birer birer artýyor veya azalýyor hýzlý þekilde..
	// artýþ azalýþ animasyonu diyebiliriz.
	// eger alinan scorelar buyukse ve birer birer artirmak sacma derece uzun suruyorsa fonksiyon icinden tempscore artis miktarini artirin.
	IEnumerator SetScoreTextAnim()
	{
		int tempScore = int.Parse(scoreText.text);
		if(tempScore < GameManager.instance.score)
		{
			while (tempScore < GameManager.instance.score)
			{
				tempScore+=5;
				scoreText.text = tempScore.ToString();
				scoreText.fontSize = 72;
				yield return new WaitForSeconds(.002f);
			}
			scoreText.fontSize = 56;

		}
		else if(tempScore > GameManager.instance.score)
		{
			while (tempScore > GameManager.instance.score)
			{
				tempScore--;
				scoreText.text = tempScore.ToString();
				yield return new WaitForSeconds(.02f);
			}
		}		
	}
	public IEnumerator heartAnim()
    {
		heartText.fontSize = 72;
		
		yield return new WaitForSeconds(.002f);
		heartText.fontSize = 56;
    }

	public void SetLevelText()
	{
		levelText.text = "LEVEL " + LevelController.instance.totalLevelNo.ToString();
	}

	public void OpenWinPanel()
	{
		winPanel.SetActive(true);
		
	}


	public void OpenLosePanel()
	{
		losePanel.SetActive(true);
	}
}
