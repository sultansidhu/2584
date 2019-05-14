using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{

    public int indRow;
    public int indCol;

    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            if (number == 0)
            {
                SetEmpty();
            } 
            else
            {
                ApplyStyle(number);
                SetVisible();
            }
        }
    }
    private int number;
    private Text tileText;
    private Image tileImage;

    private void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileImage = transform.Find("NumberPanel").GetComponent<Image>();
    }

    void ApplyStyleFromHolder(int index)
    {
        tileText.text = TileStyleHolder.Instance.tileStyles[index].Number.ToString(); // this one may have messed up
        tileText.color = TileStyleHolder.Instance.tileStyles[index].TextColor;
        tileImage.color = TileStyleHolder.Instance.tileStyles[index].TileColor;
    }

    void ApplyStyle(int num)
    {
        switch (num)
        {
            case 0:
                ApplyStyleFromHolder(0);
                break;
            case 1:
                ApplyStyleFromHolder(1);
                break;
            case 2:
                ApplyStyleFromHolder(2);
                break;
            case 3:
                ApplyStyleFromHolder(3);
                break;
            case 5:
                ApplyStyleFromHolder(4);
                break;
            case 8:
                ApplyStyleFromHolder(5);
                break;
            case 13:
                ApplyStyleFromHolder(6);
                break;
            case 21:
                ApplyStyleFromHolder(7);
                break;
            case 34:
                ApplyStyleFromHolder(8);
                break;
            case 55:
                ApplyStyleFromHolder(9);
                break;
            case 89:
                ApplyStyleFromHolder(10);
                break;
            case 144:
                ApplyStyleFromHolder(11);
                break;
            case 233:
                ApplyStyleFromHolder(12);
                break;
            case 377:
                ApplyStyleFromHolder(13);
                break;
            case 610:
                ApplyStyleFromHolder(14);
                break;
            case 987:
                ApplyStyleFromHolder(15);
                break;
            case 1597:
                ApplyStyleFromHolder(16);
                break;
            case 2584:
                ApplyStyleFromHolder(17);
                break;
            case 4181:
                ApplyStyleFromHolder(18);
                break;
            case 6765:
                ApplyStyleFromHolder(19);
                break;
            default:
                Debug.Log("Error: Invalid fibonacci number");
                break;
        }
    }

    private void SetVisible()
    {
        tileText.enabled = true;
        tileImage.enabled = true;
    }

    private void SetEmpty()
    {
        tileImage.enabled = false;
        tileText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
