using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Tile[,] AllTiles = new Tile[4, 4];
    private List<Tile[]> columns = new List<Tile[]>();
    private List<Tile[]> rows = new List<Tile[]>();
    private List<Tile> EmptyTiles = new List<Tile>();
    public int[] entries = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181 };

    // Start is called before the first frame update
    void Start()
    {
        Tile[] allTilesOneDim = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile t in allTilesOneDim)
        {
            t.Number = 0;
            AllTiles[t.indRow, t.indCol] = t;
            EmptyTiles.Add(t);
        }
        columns.Add(new Tile[] { AllTiles[0, 0], AllTiles[1, 0], AllTiles[2, 0], AllTiles[3, 0] });
        columns.Add(new Tile[] { AllTiles[0, 1], AllTiles[1, 1], AllTiles[2, 1], AllTiles[3, 1] });
        columns.Add(new Tile[] { AllTiles[0, 2], AllTiles[1, 2], AllTiles[2, 2], AllTiles[3, 2] });
        columns.Add(new Tile[] { AllTiles[0, 3], AllTiles[1, 3], AllTiles[2, 3], AllTiles[3, 3] });

        rows.Add(new Tile[] { AllTiles[0, 0], AllTiles[0, 1], AllTiles[0, 2], AllTiles[0, 3] });
        rows.Add(new Tile[] { AllTiles[1, 0], AllTiles[1, 1], AllTiles[1, 2], AllTiles[1, 3] });
        rows.Add(new Tile[] { AllTiles[2, 0], AllTiles[2, 1], AllTiles[2, 2], AllTiles[2, 3] });
        rows.Add(new Tile[] { AllTiles[3, 0], AllTiles[3, 1], AllTiles[3, 2], AllTiles[3, 3] });

        Generate();
        Generate();
    }

    private void UpdateEmptyTiles()
    {
        EmptyTiles.Clear();
        foreach(Tile t in AllTiles)
        {
            if (t.Number == 0)
            {
                EmptyTiles.Add(t);
            }
        }
    }

    void Generate()
    {
        if (EmptyTiles.Count > 0)
        {
            int indexForNewNumber = Random.Range(0, EmptyTiles.Count);
            EmptyTiles[indexForNewNumber].Number = 1;
            EmptyTiles.RemoveAt(indexForNewNumber);
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        Generate();
    //    }
    //}

    public void Move(MoveDirection direction)
    {
        ResetMerged();
        Debug.Log(direction.ToString() + " move.");
        bool movemade = false;
        for (int i = 0; i < rows.Count; i++)
        {
            switch (direction)
            {
                case MoveDirection.Down:
                    while (MakeOneMoveUpIndex(columns[i])) {
                        movemade = true;
                    }
                    break;
                case MoveDirection.Up:
                    while (MakeOneMoveDownIndex(columns[i])) { movemade = true; }
                    break;
                case MoveDirection.Left:
                    while (MakeOneMoveDownIndex(rows[i])) { movemade = true; }
                    break;
                case MoveDirection.Right:
                    while (MakeOneMoveUpIndex(rows[i])) { movemade = true;  }
                    break;
            }
        }
        if (movemade)
        {
            UpdateEmptyTiles();
            Generate();
        }
    }

    bool MakeOneMoveDownIndex(Tile[] tileLine)
    {
        for (int i = 0; i < tileLine.Length - 1; i++)
        {
            //MOVE BLOCK
            if (tileLine[i].Number == 0 && tileLine[i + 1].Number != 0)
            {
                tileLine[i].Number = tileLine[i + 1].Number;
                tileLine[i].Number = 0;
                return true;
            }
            //MERGE BLOCK
            if (tileLine[i].Number == tileLine[i + 1].Number && !tileLine[i].mergedThisTurn && !tileLine[i + 1].mergedThisTurn)
            {
                if (tileLine[i].Number < 2584)
                {
                    tileLine[i].Number = UpdateValue(tileLine[i].Number);
                    tileLine[i + 1].Number = 0;
                    tileLine[i].mergedThisTurn = true;
                    return true;
                }
                else
                {
                    Debug.Log("Game Over!");
                }
            }
        }
        return false;
    }

    int UpdateValue(int num)
    {
        for (int i = 0; i < 18; i++)
        {
            if (num == entries[i])
            {
                return i + 1;
            }
        }
        return -1;
    }

    private void ResetMerged()
    {
        foreach (Tile t in AllTiles)
        {
            t.mergedThisTurn = false;
        }
    }

    bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
    {
        for (int i = LineOfTiles.Length - 1; i > 0; i--)
        {
            //MOVE BLOCK 
            if (LineOfTiles[i].Number == 0 && LineOfTiles[i - 1].Number != 0)
            {
                LineOfTiles[i].Number = LineOfTiles[i - 1].Number;
                LineOfTiles[i - 1].Number = 0;
                return true;
            }
            // MERGE BLOCK
            if (LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i - 1].Number &&
                LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i - 1].mergedThisTurn == false)
            {
                LineOfTiles[i].Number *= 2;
                LineOfTiles[i - 1].Number = 0;
                LineOfTiles[i].mergedThisTurn = true;
                //LineOfTiles[i].PlayMergeAnimation();
                //ScoreTracker.Instance.Score += LineOfTiles[i].Number;
                //if (LineOfTiles[i].Number == 2048)
                    //YouWon();
                return true;
            }
        }
        return false;
    }
    //bool MakeOneMoveUpIndex(Tile[] tileLine)
    //{
    //    for (int i = tileLine.Length - 1; i > 0; i--)
    //    {
    //        //MOVE BLOCK
    //        if (tileLine[i].Number == 0 && tileLine[i - 1].Number != 0)
    //        {
    //            tileLine[i].Number = tileLine[i - 1].Number;
    //            tileLine[i - 1].Number = 0;
    //            return true;
    //        }
    //        //MERGE BLOCK
    //        if (tileLine[i].Number == tileLine[i - 1].Number && !tileLine[i].mergedThisTurn && !tileLine[i - 1].mergedThisTurn)
    //        {
    //            if (tileLine[i].Number < 2584)
    //            {
    //                tileLine[i].Number = entries[(tileLine[i].Number)];
    //                tileLine[i - 1].Number = 0;
    //                tileLine[i].mergedThisTurn = true;
    //                return true;
    //            }
    //            else
    //            {
    //                Debug.Log("Game Over!");
    //            }
    //        }
    //    }
    //    return false;
    //}
}
