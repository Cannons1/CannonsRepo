using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string FIRST_PRODUCT = "firstProduct";
    public static string SECOND_PRODUCT = "secondProduct";
    public static string THIRD_PRODUCT = "thirdProduct";
    public static string FOURTH_PRODUCT = "fourthProduct";
    public static string FIFTH_PRODUCT = "fifthProduct";

    [SerializeField] WriteVbles writeVbles;

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(FIRST_PRODUCT, ProductType.Consumable);
        builder.AddProduct(SECOND_PRODUCT, ProductType.Consumable);
        builder.AddProduct(THIRD_PRODUCT, ProductType.Consumable);
        builder.AddProduct(FOURTH_PRODUCT, ProductType.Consumable);
        builder.AddProduct(FIFTH_PRODUCT, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
        
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    #region Buying products
    public void BuyFirstProduct()
    {
        BuyProductID(FIRST_PRODUCT);
    }

    public void BuySecondProduct()
    {
        BuyProductID(SECOND_PRODUCT);
    }

    public void BuyThirdProduct()
    {
        BuyProductID(THIRD_PRODUCT);
    }
    public void BuyFourthProduct()
    {
        BuyProductID(FOURTH_PRODUCT);
    }
    public void BuyFifthProduct()
    {
        BuyProductID(FIFTH_PRODUCT);
    }
    #endregion

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    //  
    // --- IStoreListener
    //
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, FIRST_PRODUCT, StringComparison.Ordinal))
        {
            // The consumable item has been successfully purchased
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Singleton.instance.Coins += 150;
            PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
            writeVbles.WritingNumberOfCoins();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, SECOND_PRODUCT, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Singleton.instance.Coins += 950;
            PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
            writeVbles.WritingNumberOfCoins();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, THIRD_PRODUCT, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Singleton.instance.Coins += 1800;
            PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
            writeVbles.WritingNumberOfCoins();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, FOURTH_PRODUCT, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Singleton.instance.Coins += 3800;
            PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
            writeVbles.WritingNumberOfCoins();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, FIFTH_PRODUCT, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            Singleton.instance.Coins += 10000;
            PlayerPrefs.SetInt("Coins", Singleton.instance.Coins);
            writeVbles.WritingNumberOfCoins();
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}

