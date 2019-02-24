using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoTextControl : MonoBehaviour
{

    private Text text;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void UpdateInfo(GameObject tile) {
        EnvironmentTileControl tileControl = tile.GetComponent<EnvironmentTileControl>();
        string tileEarthTypeString = string.Format("Earth Type: {0}", tileControl.earthType);
        string tileEarthAmountString = string.Format("Earth Amount: {0}", tileControl.earth);
        string tileWaterString = string.Format("Water Amount: {0}", tileControl.water);
        string tileEarthPlusWaterString = string.Format(
            "Water Level (Earth + Water): {0}", 
            tileControl.earth + tileControl.water
        );
        string tileHeatString = string.Format("Heat: {0}", tileControl.heat);
        text.text = string.Format(
            "{0}\n{1}\n{2}\n{3}\n{4}",
            tileEarthTypeString,
            tileEarthAmountString,
            tileWaterString,
            tileEarthPlusWaterString,
            tileHeatString
        );
    }

}
