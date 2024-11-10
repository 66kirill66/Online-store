using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;


[Serializable]
public class Product
{
    public string name;
    public string description;
    public float price;
}

[Serializable]
public class ProductList
{
    public List<Product> products;
    public ProductList(List<Product> products)
    {
        this.products = products;
    }
}
[Serializable]
public class DataFetcher : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void SendNewJson(string json);

    private const string url = "https://homework.mocart.io/api/products";

    public GameObject productPrefab; 
    public Transform productsContainer;  // Container that will contain prefabs on the scene
    private float moveDistance = 0; // The distance the object will move
    private int productCount;

    public List<Product> currentListOfProducts;
    public List<GameObject> productsPrifabs = new List<GameObject>();
    void Start()
    {
        StartCoroutine(FetchProducts());
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SendNewJsonToServer()
    {
        string json = ConvertProductListToJson(currentListOfProducts);
        Debug.Log(json);

        //optional send to server

        //if (!Application.isEditor)
        //{
        //    SendNewJson(json);
        //}
    }

    IEnumerator FetchProducts()
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Request error: " + request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            ProductList productList = JsonUtility.FromJson<ProductList>(json);

            currentListOfProducts = productList.products;
            productCount = productList.products.Count;
            if (productCount == 1)
            {
                moveDistance = 5; // object in center
            }
            foreach (Product product in productList.products)
            {
                CreateProductPrefab(product);
            }
        }
    }



    void CreateProductPrefab(Product product)
    {
        GameObject prifab = GetProductPrifab(product);
        Vector3 targetPosition = productsContainer.position + Vector3.right * moveDistance;
        // Create an instance of the prefab
        GameObject productInstance = Instantiate(prifab, targetPosition, prifab.transform.rotation, productsContainer);
        productInstance.GetComponent<ProductUIManager>().Initialize(product);
        // Find Canvas
        GameObject poductCanvas = productInstance.transform.Find("PoductCanvas").gameObject;
        // Find UI elements inside the prefab and set text values
        TextMeshProUGUI nameText = poductCanvas.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descriptionText = poductCanvas.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = poductCanvas.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        //set values
        nameText.text = product.name;
        descriptionText.text = product.description;
        priceText.text = $"${product.price:F2}"; //Format the price in dollars

        moveDistance += productCount == 2 ? 10 : 5; // add offset
    }

    private GameObject GetProductPrifab(Product product)
    {
        string numberOnly = new string(product.name.Where(char.IsDigit).ToArray()); // remuve words and get only numbers from "Product 1" text

        int productNumber = int.Parse(numberOnly);
        Debug.Log("productNumber : " + productNumber);
        return productsPrifabs[productNumber - 1];
    }

    public string ConvertProductListToJson(List<Product> productList)
    {
        ProductList wrapper = new ProductList(productList);
        string json = JsonUtility.ToJson(wrapper);
        return json;
    }
}
