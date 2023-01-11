using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {

	private List<Item> database = new List<Item>();
	private JsonData itemData;

	void Awake()
	{
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase();
	}

	public Item FetchItemByID(int id)
	{
		for(int i=0;i<database.Count;i++)
			if(database[i].ID == id)
				return database[i];
		return null;
	}

	void ConstructItemDatabase()
	{
		for(int i = 0; i < itemData.Count; i++){
			database.Add(new Item((int)itemData[i]["id"], (string)itemData[i]["name"], (string)itemData[i]["title"], (int)itemData[i]["stats"]["damage"], (int)itemData[i]["stats"]["defense"], (bool)itemData[i]["isGear"], (string)itemData[i]["armor"], (bool)itemData[i]["hasActiveSelectable"], (int)itemData[i]["durability"], (string)itemData[i]["description"]));
		}
	}
}

public class Item
{
	public int ID {get; set;}
	public string Name {get; set;}
	public string Title {get; set;}
	public int Damage {get; set;}
	public int Defense {get; set;}
	public bool IsGear {get; set;}
	public string Armor {get; set;}
	public bool HasActiveSelectable {get; set;}
	public int Durability {get; set;}
	public string Description {get; set;}
	public Sprite Sprite {get; set;}


	public Item(int id, string name, string title, int damage, int defense, bool isGear, string armor, bool hasActiveSelectable, int durability, string description)
	{
		this.ID = id;
		this.Name = name;
		this.Title = title;
		this.Damage = damage;
		this.Defense = defense;
		this.IsGear = isGear;
		this.Armor = armor;
		this.HasActiveSelectable = hasActiveSelectable;
		this.Durability = durability;
		this.Description = description;

		this.Sprite = Resources.Load<Sprite>("Items/" + name);
	}

	public Item()
	{
		this.ID = -1;
	}
}