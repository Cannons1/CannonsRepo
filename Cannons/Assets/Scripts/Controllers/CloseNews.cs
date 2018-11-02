using UnityEngine;

public class CloseNews : MonoBehaviour {
    [SerializeField] Animator animatorNews;

    int isActive;

    private void Start()
    {
        //PlayerPrefs.DeleteKey("News");
        if (PlayerPrefs.HasKey("News"))
        {
            isActive = PlayerPrefs.GetInt("News");
            if (isActive == 0)
                gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void SetAnimatorClosed()
    {
        animatorNews.SetBool("Closed", true);
        PlayerPrefs.SetInt("News", 0);
    }
}