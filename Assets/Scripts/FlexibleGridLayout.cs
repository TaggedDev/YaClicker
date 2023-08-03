using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
     
    [SerializeField] private Vector2 spacing;
    private Vector2 _cellSize;
    private int _rows;
    private int _columns;

    public override void SetLayoutHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        float root = Mathf.Sqrt(transform.childCount);
        _rows = Mathf.CeilToInt(root);
        _columns = Mathf.CeilToInt(root);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / _columns;
        float cellHeight = parentHeight / _rows;

        _cellSize.x = cellWidth;
        _cellSize.y = cellHeight;

        int columnCount = 0, rowCount = 0;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / _columns;
            columnCount = i % _columns;

            var item = rectChildren[i];
            var xPos = (_cellSize.x * columnCount) + (spacing.x * columnCount);
            var yPos = (_cellSize.y * rowCount) + (spacing.y * rowCount);
            
            SetChildAlongAxis(item, 0, xPos, _cellSize.x);
            SetChildAlongAxis(item, 1, yPos, _cellSize.y);

        }
    }
    
    public override void CalculateLayoutInputVertical()
    {
       
        
        
    }

    public override void SetLayoutVertical()
    {
        throw new System.NotImplementedException();
    }
}
