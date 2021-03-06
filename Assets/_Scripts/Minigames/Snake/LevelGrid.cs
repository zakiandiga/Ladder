using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{

    private Vector2 foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

    }

    public void Setup(Snake snake)
    {
        this.snake = snake;

        SpawnFood();
    }
    
    private void SpawnFood()
    {
        do
        {
            foodGridPosition = new Vector2(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1); //Randomizes food spawn position and prevents the food from spawning on top of the snake

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));

        //We need to set the layer in Food's sprite renderer, so we'll put the SpriteRenderer on a variable

        var foodObject = foodGameObject.GetComponent<SpriteRenderer>();
        foodObject.sprite = GameAssets.i.foodSprite;
        foodObject.sortingLayerName = "MinigameFG";       
        
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        
      
    }

    public bool TrySnakeEatFood(Vector2 snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.AddScore();
            return true;
        } else
        {
            return false;
        }
    }

}
