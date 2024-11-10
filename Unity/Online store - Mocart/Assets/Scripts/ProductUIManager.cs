using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProductUIManager : MonoBehaviour
{
    public Canvas menutCanvas; 
    public TMP_InputField nameInputField; 
    public TMP_InputField priceInputField; 
    public Button saveButton; 
    public Button close; 

    public GameObject productPrifab; //prefab for rotation

    private bool isRotating;
    private Quaternion startTransform;

    public GameObject confirmationMessage;

    public Product productData;

    private DataFetcher dataFetcher;

    void Start()
    {
        CloseMenu();
        dataFetcher = FindObjectOfType<DataFetcher>();
        startTransform = productPrifab.transform.rotation;
        // Subscribe to the button click event
        saveButton.onClick.AddListener(SaveChanges);
        close.onClick.AddListener(CloseMenu);
    }

    public void Initialize(Product product)
    {
        productData = product;
        UpdateUI();
    }
    void UpdateUI()
    {
        if (productData != null)
        {
            nameInputField.text = productData.name;
            priceInputField.text = productData.price.ToString("F2");
        }
    }

    void OnMouseDown()
    {
        if (!menutCanvas.enabled)
        {
            menutCanvas.enabled = true;
        }
    }
    void Update()
    {
        if (isRotating)
        {
            productPrifab.transform.Rotate(0f, 0f, 30 * Time.deltaTime);
        }
    }

    void OnMouseEnter()
    {
        isRotating = true;
    }

    void OnMouseExit()
    {
        isRotating = false;
        productPrifab.transform.rotation = startTransform;
    }
    public void CloseMenu()
    {
        menutCanvas.enabled = false;
        confirmationMessage.SetActive(false);
    }


    void SaveChanges()
    {
        if (productData != null)
        {
            bool hasChanges = false;

            // Check and update product name
            if (productData.name != nameInputField.text)
            {
                productData.name = nameInputField.text;
                hasChanges = true;
            }
            // Check and update product price
            if (float.TryParse(priceInputField.text, out float newPrice))
            {
                if (!ArePricesEqual(productData.price, newPrice))
                {
                    productData.price = newPrice;
                    hasChanges = true;
                }
            }
            else
            {
                Debug.LogWarning("Error: Invalid price format.");
                return;
            }

            if (hasChanges)
            {
                UpdateProductDisplay();
                dataFetcher.SendNewJsonToServer();
                confirmationMessage.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Error: Product data is not set.");
            return;
        }
        Invoke(nameof(CloseMenu), 0.5f);
    }
    

    // Method for updating prefab UI after changes
    void UpdateProductDisplay()
    {
        GameObject poductCanvas = gameObject.transform.Find("PoductCanvas").gameObject;
        TextMeshProUGUI nameText = poductCanvas.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI priceText = poductCanvas.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

        nameText.text = productData.name;
        priceText.text = $"${productData.price:F2}";
    }

    bool ArePricesEqual(float price1, float price2, float tolerance = 0.01f)
    {
        return Mathf.Abs(price1 - price2) < tolerance;
    }
}

