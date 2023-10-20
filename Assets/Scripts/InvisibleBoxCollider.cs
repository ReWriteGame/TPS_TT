using UnityEngine;

public class InvisibleBoxCollider : MonoBehaviour
{
    [SerializeField] private Vector3 sizeBox;
    [SerializeField] private Vector3 centerBox;
    [SerializeField][Min(0)] private float wallThickness = 1;

    private Vector3 radius;
    private const int countBorder = 6;
    private BoxCollider[] colliders = new BoxCollider[countBorder];

    [SerializeField] private bool useRight = true;
    [SerializeField] private bool useLeft = true;
    [SerializeField] private bool useBack = true;
    [SerializeField] private bool useFront = true;
    [SerializeField] private bool useTop = true;
    [SerializeField] private bool useDown = true;

    private void Awake()
    {
        radius = sizeBox / 2;
        GenarateAllBox();
        UpdateBorderSettings();
    }

    private void UpdateBorderSettings()
    {
        if (useRight) GenerateRight();
        if (useLeft) GenerateLeft();
        if (useFront) GenerateFront();
        if (useBack) GenerateBack();
        if (useDown) GenerateGround();
        if (useTop) Generate—eiling();
    }

    private void Generate—eiling()
    {
        Vector3 position = centerBox + radius.y * Vector3.up;
        UpdateBorderSettings(0, new Vector3(sizeBox.x, wallThickness, sizeBox.z), position);
    }

    private void GenerateGround()
    {
        Vector3 position = centerBox + radius.y * Vector3.down;
        UpdateBorderSettings(1, new Vector3(sizeBox.x, wallThickness, sizeBox.z), position);
    }

    private void GenerateRight()
    {
        Vector3 position = centerBox + radius.x * Vector3.right;
        UpdateBorderSettings(2, new Vector3(wallThickness, sizeBox.y, sizeBox.z), position);
    }

    private void GenerateLeft()
    {
        Vector3 position = centerBox + radius.x * Vector3.left;
        UpdateBorderSettings(3, new Vector3(wallThickness, sizeBox.y, sizeBox.z), position);
    }

    private void GenerateBack()
    {
        Vector3 position = centerBox + radius.z * Vector3.back;
        UpdateBorderSettings(4, new Vector3(sizeBox.x, sizeBox.y, wallThickness), position);
    }

    private void GenerateFront()
    {
        Vector3 position = centerBox + radius.z * Vector3.forward;
        UpdateBorderSettings(5, new Vector3(sizeBox.x, sizeBox.y, wallThickness), position);
    }

    private void GenarateAllBox()
    {
        for (int i = 0; i < countBorder; i++)
            colliders[i] = gameObject.AddComponent<BoxCollider>();
    }

    private void UpdateBorderSettings(int index, Vector3 size, Vector3 position)
    {
        colliders[index].size = size;
        colliders[index].center = position;
    }
}
