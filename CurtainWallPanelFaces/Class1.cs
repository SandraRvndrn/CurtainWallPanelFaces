using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CurtainWallPanelFaces
{
    [Transaction(TransactionMode.Manual)]
    public class Class1 : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //Get UIDocument & Document
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Autodesk.Revit.DB.Document doc = uidoc.Document;

            List<Element> elements1 = new List<Element>();
            List<XYZ> vertices = new List<XYZ>();
            string name;
            

            FilteredElementCollector collector
                  = new FilteredElementCollector(doc)
                  .OfCategory(BuiltInCategory.INVALID)
                  .WhereElementIsNotElementType();

                foreach (Element e in collector)
                {
                    if (null != e.Category
                      && e.Category.HasMaterialQuantities)
                    {
                        elements1.Add(e);
                    }
                }

            //WINDOW
            for (int i = 0; i < elements1.Count; i++)
            {
                //Display Element ID
                if (elements1 != null)
                {
                    vertices = new List<XYZ>();

                    //Retieving element( Door )
                    ElementId eleid = elements1[i].Id;
                    Element ele = doc.GetElement(eleid);
                    name = ele.Name.ToString();

                    //getting vertex of Door

                    Options opt = new Options();
                    GeometryElement geomElem1 = ele.get_Geometry(opt);
                    if (geomElem1.Cast<GeometryInstance>() != null)
                    {
                        var geometrySolids = geomElem1.Cast<GeometryInstance>().FirstOrDefault().GetInstanceGeometry();

                        foreach (GeometryObject geomObj1 in geometrySolids)
                        {
                            Solid geomSolid1 = geomObj1 as Solid;
                            if (null != geomSolid1)
                            {
                                int faces = 0;
                                foreach (Face geomFace1 in geomSolid1.Faces)
                                {
                                    faces++;
                                    Mesh mesh = geomFace1.Triangulate();
                                    for (int m = 0; m < mesh.NumTriangles; m++)
                                    {
                                        MeshTriangle triangle = mesh.get_Triangle(m);
                                        vertices.Add(triangle.get_Vertex(0));
                                        vertices.Add(triangle.get_Vertex(1));
                                        vertices.Add(triangle.get_Vertex(2));
                                    }
                                }
                            }
                        }
                        //creating and 
                        double[] arrayPositionClass = new double[vertices.Count * 3];

                        for (int l = 0; l < vertices.Count; l++)
                        {
                            arrayPositionClass[l * 3] = vertices[l].X;
                            arrayPositionClass[(l * 3) + 1] = vertices[l].Z;
                            arrayPositionClass[(l * 3) + 2] = vertices[l].Y;
                        }
                    }
                    else
                    {
                        foreach (GeometryObject geomObj1 in geomElem1)
                        {
                            Solid geomSolid1 = geomObj1 as Solid;
                            if (null != geomSolid1)
                            {
                                int faces = 0;
                                foreach (Face geomFace1 in geomSolid1.Faces)
                                {
                                    faces++;
                                    Mesh mesh = geomFace1.Triangulate();
                                    for (int m = 0; m < mesh.NumTriangles; m++)
                                    {
                                        MeshTriangle triangle = mesh.get_Triangle(m);
                                        vertices.Add(triangle.get_Vertex(0));
                                        vertices.Add(triangle.get_Vertex(1));
                                        vertices.Add(triangle.get_Vertex(2));
                                    }
                                }
                            }
                        }
                        //creating and 
                        double[] arrayPositionClass = new double[vertices.Count * 3];

                        for (int l = 0; l < vertices.Count; l++)
                        {
                            arrayPositionClass[l * 3] = vertices[l].X;
                            arrayPositionClass[(l * 3) + 1] = vertices[l].Z;
                            arrayPositionClass[(l * 3) + 2] = vertices[l].Y;
                        }
                    }
                    
                }

                //positionClass = new position()
                //{
                //    itemSize = 3,
                //    type = "Float32Array",
                //    array = arrayPositionClass,
                //    normalized = false
                //};

                //attributes = new attributes()
                //{
                //    position = positionClass
                //};

                //int[] arrayIndex = new int[vertices.Count];
                //int b = vertices.Count - 1;
                //for (int a = 0; a < vertices.Count; a++)
                //{
                //    arrayIndex[a] = b;
                //    b--;
                //}

                //index = new index()
                //{
                //    type = "Uint16Array",
                //    array = arrayIndex
                //};

                //double[] center = { -1.7954902648925781, 6.561679840087891, -16.903955459594727 };
                //boundingSphere = new boundingSphere()
                //{
                //    center = center,
                //    radius = 36.684208412733405
                //};

                //data = new data()
                //{
                //    attributes = attributes,
                //    index = index,
                //    boundingSphere = boundingSphere
                //};

                //string id = "4d106917-1013-4d8f-ad0c-2bcf4819cbg" + (Window.Count + i);
                //double[] arrayForclass = new double[vertices.Count];

                //Geometries geo1 = new Geometries()
                //{
                //    uuid = id,
                //    type = "BufferGeometry",
                //    data = data
                //};

                //geometriesWindow.Add(geo1);

                //ma = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };
                //up = new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 };

                //string geom = "4d106917-1013-4d8f-ad0c-2bcf4819cbg" + (i + Window.Count);
                //string uid = "8a366657-d573-4f02-a870-45212ac3cde" + (i + Window.Count);

                //Children ch1 = new Children()
                //{
                //    uuid = uid,
                //    type = "Mesh",
                //    name = name,
                //    layers = 1,
                //    matrix = ma,
                //    up = up,
                //    geometry = geom,
                //    material = "6d30d42d-f397-4a0e-9745-e68f0265a500",
                //};

                //childrenWindow.Add(ch1);

                //Material matWindow = new Material()
                //{
                //    uuid = "6d30d42d-f397-4a0e-9745-e68f0265a500",
                //    type = "MeshStandardMaterial",
                //    color = 6863562,
                //    roughness = 1,
                //    metalness = 0,
                //    emissive = 0,
                //    envMapIntensity = 1,
                //    depthFunc = 3,
                //    depthTest = true,
                //    depthWrite = true,
                //    colorWrite = true,
                //    stencilWrite = false,
                //    stencilWriteMask = 255,
                //    stencilFunc = 519,
                //    stencilRef = 0,
                //    stencilFuncMask = 255,
                //    stencilFail = 7680,
                //    stencilZFail = 7680,
                //    stencilZPass = 7680,
                //    flatShading = true
                //};
                //mat.Add(matWindow);
            }


            return Result.Succeeded;
            

           
        }
    }
}
