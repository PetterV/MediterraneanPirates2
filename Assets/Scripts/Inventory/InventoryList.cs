using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine.UI;

public class InventoryList
{
    private List<InventoryItem> inventoryItems;
    public List<InventoryItem> GetInventoryItems()
	{
		if (inventoryItems == null)
			LoadXml();
		return inventoryItems;
	}

	private void LoadXml()
	{
		string filePath = Path.Combine (Application.streamingAssetsPath, "InventoryItems.xml");
        inventoryItems =
            (
                from a in XDocument.Load(filePath).Root.Elements("item")
                select new InventoryItem
                {
                    id = (int)a.Element("id"),
                    itemName = (string)a.Element("itemName"),
                    baseValue = (float)a.Element("baseValue"),
                    minValue = (float)a.Element("minValue"),
                    maxValue = (float)a.Element("maxValue"),
                    iconID = (int)a.Element("iconID"),
                    portList = (string)a.Element("port")
				}
			).ToList();

        foreach(InventoryItem item in inventoryItems){
            item.ports = new List<int>();
            string[] portListNumbers = item.portList.Split(',');
            foreach (string s in portListNumbers){
                item.ports.Add(int.Parse(s));
            }
        }
	}
}

public class InventoryItem
{
    public int id;
    public List<int> ports;
    public string portList;
    public string itemName;
    public float baseValue;
    public float minValue;
    public float maxValue;
    public int iconID;
    public Sprite icon;
}
