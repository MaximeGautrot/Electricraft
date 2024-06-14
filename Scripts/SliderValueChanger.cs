using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValueChanger : MonoBehaviour
{
    public ElecManager elecManager;
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public TextMeshProUGUI sliderValueText1;
    public TextMeshProUGUI sliderValueText2;
    public TextMeshProUGUI sliderValueText3;

    void Start()
    {
        slider1.onValueChanged.AddListener(delegate { ValueChangeCheck1(); });

        slider2.onValueChanged.AddListener(delegate { ValueChangeCheck2(); });

        slider3.onValueChanged.AddListener(delegate { ValueChangeCheck3(); });

        slider1.value = elecManager.GetN1();
        slider2.value = elecManager.GetN2();
        slider1.value = elecManager.GetN3();

        UpdateSliderValueText(); // Ajoutez cette ligne
    }

    void Update()
    {
        UpdateSliderValueText();
    }

    void ValueChangeCheck1()
    {
        elecManager.SetMyValue1(Mathf.RoundToInt(slider1.value));
        UpdateSliderValueText(); // Ajoutez cette ligne
    }

    void ValueChangeCheck2()
    {
        elecManager.SetMyValue2(Mathf.RoundToInt(slider2.value));
        UpdateSliderValueText(); // Ajoutez cette ligne
    }

    void ValueChangeCheck3()
    {
        elecManager.SetMyValue3(Mathf.RoundToInt(slider3.value));
        UpdateSliderValueText(); // Ajoutez cette ligne
    }

    void UpdateSliderValueText() // Ajoutez cette méthode
    {
        sliderValueText1.text = Mathf.RoundToInt(slider1.value).ToString(); // Remplacez "slider1" par le slider que vous souhaitez afficher
        sliderValueText2.text = Mathf.RoundToInt(slider2.value).ToString();
        sliderValueText3.text = Mathf.RoundToInt(slider3.value).ToString();
    }
}
