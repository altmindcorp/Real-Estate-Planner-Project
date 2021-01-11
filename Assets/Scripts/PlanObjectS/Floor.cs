using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : PlanObject
{
    public List<Vector3> vertices = new List<Vector3>();
    FloorObjectData floorData;
    private Vector3 initialScale;
    public FloorAnchor anchorPrefab;
    public float floorArea = 0;
    public List<Vector3> anchorsPosition = new List<Vector3>();

    public void UpdateFloor(int mode, Vector3 mousePosition)
    {
        if (mode == 0)
        {
            
            if (mousePosition.x > meshFilter.mesh.bounds.max.x)
            {
                Vector3 newVertice2 = new Vector3(vertices[2].x + GridScaler.scaleValue,vertices[2].y, vertices[2].z);
                Vector3 newVertice3 = new Vector3(vertices[3].x + GridScaler.scaleValue, vertices[3].y, vertices[3].z);
                vertices[2] = newVertice2;
                vertices[3] = newVertice3;
            }

            else if (mousePosition.y > meshFilter.mesh.bounds.max.y)
            {
                Vector3 newVertice1 = new Vector3(vertices[1].x, vertices[1].y + GridScaler.scaleValue, vertices[1].z);
                Vector3 newVertice2 = new Vector3(vertices[2].x, vertices[2].y + GridScaler.scaleValue, vertices[2].z);
                vertices[1] = newVertice1;
                vertices[2] = newVertice2;
            }

            else if (mousePosition.x < meshFilter.mesh.bounds.min.x - GridScaler.scaleValue)
            {
                Vector3 newVertice1 = new Vector3(vertices[1].x - GridScaler.scaleValue, vertices[1].y, vertices[1].z);
                Vector3 newVertice0 = new Vector3(vertices[0].x - GridScaler.scaleValue, vertices[0].y, vertices[0].z);
                vertices[1] = newVertice1;
                vertices[0] = newVertice0;
            }

            else if (mousePosition.y < meshFilter.mesh.bounds.min.y - GridScaler.scaleValue)
            {
                Vector3 newVertice0 = new Vector3(vertices[0].x, vertices[0].y - GridScaler.scaleValue, vertices[0].z);
                Vector3 newVertice3 = new Vector3(vertices[3].x, vertices[3].y - GridScaler.scaleValue, vertices[3].z);
                vertices[0] = newVertice0;
                vertices[3] = newVertice3;
            }
        }

        ResizeMesh();
    }

    private void ResizeMesh()
    {
        meshFilter.mesh.vertices = vertices.ToArray();
        meshCollider.sharedMesh = meshFilter.mesh;
        meshFilter.mesh.RecalculateBounds();
        floorData.ChangeMeshProperties(this.meshFilter.mesh);
        floorData.anchorVertices = this.anchorsPosition.ToArray();
    }

    new public void CreatePlanObject()
    {
        
        this.meshFilter.mesh = MeshCreator.Create2DMesh(ObjectsParams.scale, 0);
        
    }

    public override void OnMouseDown()
    {
        ObjectsDataRepository.currentFloorArea = GetFloorArea(this.meshFilter.mesh.vertices);
    }

    private void AddAnchors()
    {
        FloorAnchor anchor;
        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.transform.Translate(new Vector3(0.05f, 0.05f, -0.0001f), Space.World);
        anchor.verticeNumber = 0;
        anchor.floor = this;
        anchorsPosition.Add(anchor.transform.position);

        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.transform.Translate(new Vector3(0.05f, -0.05f + 1, -0.0001f), Space.World);
        anchor.verticeNumber = 1;
        anchor.floor = this;
        anchorsPosition.Add(anchor.transform.position);

        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.transform.Translate(new Vector3(-0.05f + 1, -0.05f + 1, -0.0001f), Space.World);
        anchor.verticeNumber = 2;
        anchor.floor = this;
        anchorsPosition.Add(anchor.transform.position);

        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.transform.Translate(new Vector3(-0.05f + 1, 0.05f, -0.0001f), Space.World);
        anchor.verticeNumber = 3;
        anchor.floor = this;
        anchorsPosition.Add(anchor.transform.position);
    }

    private void ReAddAnchors()
    {
        FloorAnchor anchor;
        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.verticeNumber = 0;
        anchor.floor = this;
        anchor.transform.position = anchorsPosition[0];
        Debug.Log("Floor Anchor Position: " + anchorsPosition[0]);

        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.verticeNumber = 1;
        anchor.floor = this;
        anchor.transform.position = anchorsPosition[1];
        Debug.Log("Floor Anchor Position: " + anchorsPosition[1]);

        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.verticeNumber = 2;
        anchor.floor = this;
        anchor.transform.position = anchorsPosition[2];
        Debug.Log("Floor Anchor Position: " + anchorsPosition[2]);

        anchor = Instantiate(anchorPrefab, this.transform, false);
        anchor.verticeNumber = 3;
        anchor.floor = this;
        anchor.transform.position = anchorsPosition[3];
        Debug.Log("Floor Anchor Position: " + anchorsPosition[3]);
    }
    public override void AddAdditionalValues()
    {
        initialScale = new Vector3 (1,1,0);

        AddAnchors();
        SetFloorArea();
        floorData = new FloorObjectData(this.meshFilter.mesh, this.transform.position, this.anchorsPosition.ToArray(), this.initialScale, this.id);
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Add(floorData);
    }

    public override void OnMouseDrag()
    {
        //throw new System.NotImplementedException();
    }

    public void ChangeVerticePosition(int verticeNumber, Vector3 positionChange)
    {
        this.meshFilter.mesh = MeshCreator.ChangeFloorMesh(this.meshFilter.mesh, verticeNumber, positionChange, initialScale);
        ObjectsDataRepository.currentSaveFile.planObjectsDataList.Find(x => x.id == this.id).ChangeMeshProperties(this.meshFilter.mesh);
        
    }

    public void ChangeFloorDataAnchorPosition(Vector3 position, int anchorNumber)
    {
        floorData.anchorVertices[anchorNumber] = position;
    }

    public override void ReAddValues(PlanObjectData planObjData)
    {

        floorData = planObjData as FloorObjectData;
        meshFilter.mesh.vertices = floorData.GetVertices();
        anchorsPosition.Clear();
        foreach (Vector3 v in floorData.anchorVertices)
        {
            anchorsPosition.Add(v);
            Debug.Log("Floor Anchor Position: " + v);
        }
        ReAddAnchors();
        this.initialScale = floorData.initialScale;
        SetFloorArea();
    }

    private float GetFloorArea(Vector3[] vertices)
    {
        var length = vertices.Length;
        Debug.Log("Floor Vertices Length: " + vertices.Length);
        float floorArea = 0;
        for (int i = 0; i < length-1; i++)
        {
            floorArea += vertices[i].x * vertices[i + 1].y - vertices[i + 1].x * vertices[i].y;
        }

        floorArea += vertices[length - 1].x * vertices[0].y - vertices[0].x * vertices[length - 1].y;
        return Mathf.Abs(floorArea) / 2;
    }

    public void SetFloorArea()
    {
        ObjectsDataRepository.currentFloorArea = GetFloorArea(meshFilter.mesh.vertices);
    }
}
