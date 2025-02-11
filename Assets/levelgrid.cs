using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelgrid {

    private Vector2Int foodGridPosition;
    private int width;
    private int height;


    public Levelgrid(int width, int height){
        this.width = width;
        this.height = height;

    }

    private void SpawnFood(){
        foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

        GameObject foodGameObject = new GameObject("food", typeof(SpriteRenderer));
    }

}
