using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public GameObject resourcesTab;
    public Button resourcesBtn;
    public Image resourcesImg;
    public TMP_Text resourcesText;

    public GameObject gachaTab;
    public Button gachaBtn;
    public Image gachaImg;
    public TMP_Text gachaText;

    public TMP_Text tabText;
    void Start()
    {
        resourcesBtn.onClick.AddListener(() => ChangeTab(true));
        gachaBtn.onClick.AddListener(() => ChangeTab(false));
    }

    private void ChangeTab(bool isResourcesTab)
    {
        resourcesTab.gameObject.SetActive(isResourcesTab);
        resourcesImg.color = isResourcesTab ? ColorPalette.ultramarine : Color.white;
        resourcesText.color = isResourcesTab ? Color.white : ColorPalette.font;

        gachaTab.gameObject.SetActive(false == isResourcesTab);
        gachaImg.color = false == isResourcesTab ? ColorPalette.ultramarine : Color.white;
        gachaText.color = false == isResourcesTab ? Color.white : ColorPalette.font;

        tabText.text = isResourcesTab ? "자원" : "뽑기";
    }
}
