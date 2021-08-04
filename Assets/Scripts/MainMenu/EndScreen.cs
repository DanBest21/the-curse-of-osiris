using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI evidenceCollected;

    private bool hasSetup = false;

    public void Update()
    {
        if (!hasSetup)
        {
            evidenceCollected.text = "Evidence Collected: " + GameManager.Instance.CollectedEvidence.Count + "/" + (Resources.LoadAll<Evidence>("Evidence").Length - Resources.LoadAll<Evidence>("Evidence/Utility").Length);
            hasSetup = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}