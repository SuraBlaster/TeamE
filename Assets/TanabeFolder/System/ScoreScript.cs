using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class ScoreScript : MonoBehaviour
{
    [SerializeField]
    //Text score_text;
    TextMeshProUGUI score_text;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = string.Format("{0:D6}", score);
    }
}
