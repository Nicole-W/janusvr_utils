﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UnityEngine.FBX
{
    public static class FBXExporter
    {
        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static void Initialize([MarshalAs(UnmanagedType.LPStr)] string SceneName);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static void SetFBXCompatibility(int CompatibilityVersion);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static void AddMesh([MarshalAs(UnmanagedType.LPStr)] string MeshName);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddVertices(FbxVector3[] Vertices, int Count);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddIndices(int[] Triangles, int Count, int Material);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddNormals(FbxVector3[] Normals, int Count);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddTexCoords(FbxVector2[] TexCoords, int Count, int UVLayer, string ChannelName);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl)]
        public extern static void AddMaterial(FbxVector3 DiffuseColor);

        [DllImport("UnityFBXExporter", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static void Export([MarshalAs(UnmanagedType.LPStr)] string SceneName);

        public static void ExportMesh(Mesh mesh, string path, int fbxVersion = 1)
        {
            FBXExporter.Initialize(mesh.name);
            FBXExporter.SetFBXCompatibility(fbxVersion);
            FBXExporter.AddMesh(mesh.name);

            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;
            int[] triangles = mesh.triangles;

            FbxVector3[] nvertices = new FbxVector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = vertices[i];
                nvertices[i] = new FbxVector3(v.x, v.y, v.z);
            }

            FbxVector3[] nnormals = new FbxVector3[triangles.Length];
            for (int i = 0; i < triangles.Length; i++)
            {
                Vector3 v = normals[triangles[i]];
                nnormals[i] = new FbxVector3(v.x, v.y, v.z);
            }

            FBXExporter.AddMaterial(new FbxVector3(0.7, 0.7, 0.7));
            FBXExporter.AddIndices(triangles, triangles.Length, 0);
            FBXExporter.AddVertices(nvertices, nvertices.Length);
            FBXExporter.AddNormals(nnormals, nnormals.Length);

            for (int i = 0; i < 4; i++)
            {
                List<Vector2> tverts = new List<Vector2>();
                mesh.GetUVs(i, tverts);

                if (tverts.Count == 0)
                {
                    continue;
                }

                FbxVector2[] uv = new FbxVector2[triangles.Length];
                for (int j = 0; j < triangles.Length; j++)
                {
                    Vector2 v = tverts[triangles[j]];
                    uv[j] = new FbxVector2(v.x, v.y);
                }

                FBXExporter.AddTexCoords(uv, uv.Length, i, "UV" + i);
            }

            FBXExporter.Export(path);
        }

        public static void ExportMeshPOG(Mesh mesh, string path, int fbxVersion = 1)
        {
            FBXExporter.Initialize(mesh.name);
            FBXExporter.SetFBXCompatibility(fbxVersion);
            FBXExporter.AddMesh(mesh.name);

            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;
            int[] triangles = mesh.triangles;

            FbxVector3[] nvertices = new FbxVector3[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = vertices[i];
                nvertices[i] = new FbxVector3(v.x, v.y, v.z);
            }

            FbxVector3[] nnormals = new FbxVector3[triangles.Length];
            for (int i = 0; i < triangles.Length; i++)
            {
                Vector3 v = normals[triangles[i]];
                nnormals[i] = new FbxVector3(v.x, v.y, v.z);
            }

            FBXExporter.AddMaterial(new FbxVector3(0.7, 0.7, 0.7));
            FBXExporter.AddIndices(triangles, triangles.Length, 0);
            FBXExporter.AddVertices(nvertices, nvertices.Length);
            FBXExporter.AddNormals(nnormals, nnormals.Length);

            for (int i = 0; i < 4; i++)
            {
                i = 1;
                List<Vector2> tverts = new List<Vector2>();
                mesh.GetUVs(i, tverts);

                if (tverts.Count == 0)
                {
                    continue;
                }

                FbxVector2[] uv = new FbxVector2[triangles.Length];
                for (int j = 0; j < triangles.Length; j++)
                {
                    Vector2 v = tverts[triangles[j]];
                    uv[j] = new FbxVector2(v.x, v.y);
                }

                i = 0;
                FBXExporter.AddTexCoords(uv, uv.Length, i, "UV" + i);

                break;
            }

            FBXExporter.Export(path);
        }
    }
}
