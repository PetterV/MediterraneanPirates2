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
                    iconID = (int)a.Element("iconID")
				}
			).ToList();
	}
}

public class InventoryItem
{
    public int id;
    public string itemName;
    public float baseValue;
    public int iconID;
    public Sprite icon;
}
