using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConectorHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 900f)] public float length;
    [Range(-25f, 25f)] public float rotation;
}

public class UI_TreeConectorHandler : MonoBehaviour
{
    private RectTransform myRect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectDetails[] connectionDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private Image connectionImage;
    private Color originalColor;

    void Awake()
    {
        if (connectionImage != null)
        {
            originalColor = connectionImage.color;
        }
    }

    private void OnValidate()
    {
        if (connectionDetails.Length <= 0)
        {
            return;
        }

        if (connectionDetails.Length != connections.Length)
        {
            Debug.LogWarning("Details and Connections arrays must have the same length.", this);
            return;
        }

        UpdateConnection();
    }

    public void UpdateConnection()
    {
        for (int i = 0; i < connectionDetails.Length; i++)
        {
            var detail = connectionDetails[i];
            var connection = connections[i];

            Vector2 targetPosition = connection.GetConnectionPoint(myRect);
            Image connectionImage = connection.GetConnectionImage();

            connection.DirectConnection(detail.direction, detail.length, detail.rotation);

            if (detail.childNode == null)
            {
                continue;
            }

            detail.childNode.SetPosition(targetPosition);
            detail.childNode.SetConnectionImage(connectionImage);
            detail.childNode.transform.SetAsLastSibling();
        }
    }

    public void UpdateAllConnections()
    {
        UpdateConnection();

        foreach (var node in connectionDetails)
        {
            if (node.childNode == null)
            {
                continue;
            }

            node.childNode.UpdateAllConnections();
        }
    }

    public void UnlockConnectionImage(bool unlocked)
    {
        if (connectionImage == null)
        {
            return;
        }

        connectionImage.color = unlocked ? Color.white : originalColor;
    }

    public void SetConnectionImage(Image image) => connectionImage = image;

    public void SetPosition(Vector2 position) => myRect.anchoredPosition = position;
}
