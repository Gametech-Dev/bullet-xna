﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BulletXNA.BulletCollision.CollisionDispatch;
using BulletXNA.BulletCollision.BroadphaseCollision;
using Microsoft.Xna.Framework;
using BulletXNA.BullettCollision.CollisionShapes;
using BulletXNA.BulletCollision.CollisionShapes;
using BulletXNA.BulletDynamics.Dynamics;
using BulletXNA.BulletDynamics.ConstraintSolver;
using BulletXNA.LinearMath;
using System.IO;
using BulletXNA;

namespace BulletXNADemos.Demos
{
	public class LargeMeshDemo : DemoApplication
	{
		const float SCALING = 1.0f;
		public override void InitializeDemo()
		{
			base.InitializeDemo();
			SetCameraDistance(SCALING * 50f);

			//string filename = @"C:\users\man\bullett\xna-largemesh-output.txt";
			//FileStream filestream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
			//BulletGlobals.g_streamWriter = new StreamWriter(filestream);

			///collision configuration contains default setup for memory, collision setup
			m_collisionConfiguration = new DefaultCollisionConfiguration();

			///use the default collision dispatcher. For parallel processing you can use a diffent dispatcher (see Extras/BulletMultiThreaded)
			m_dispatcher = new CollisionDispatcher(m_collisionConfiguration);

			m_broadphase = new DbvtBroadphase();
			IOverlappingPairCache pairCache = null;
			//pairCache = new SortedOverlappingPairCache();

			m_broadphase = new SimpleBroadphase(1000, pairCache);

			///the default constraint solver. For parallel processing you can use a different solver (see Extras/BulletMultiThreaded)
			SequentialImpulseConstraintSolver sol = new SequentialImpulseConstraintSolver();
			m_constraintSolver = sol;

			m_dynamicsWorld = new DiscreteDynamicsWorld(m_dispatcher, m_broadphase, m_constraintSolver, m_collisionConfiguration);

			Vector3 gravity = new Vector3(0, -10, 0);
			m_dynamicsWorld.SetGravity(ref gravity);

			///create a few basic rigid bodies
			Vector3 halfExtents = new Vector3(50, 50, 50);
			//Vector3 halfExtents = new Vector3(10, 10, 10);
			//CollisionShape groundShape = new BoxShape(ref halfExtents);
			//CollisionShape groundShape = new StaticPlaneShape(Vector3.Up, 50);
			CollisionShape groundShape = BuildLargeMesh();
			m_collisionShapes.Add(groundShape);

			Matrix groundTransform = Matrix.CreateTranslation(new Vector3(0, -10, 0));
			//Matrix groundTransform = Matrix.CreateTranslation(new Vector3(0,-10,0));
			float mass = 0f;
			LocalCreateRigidBody(mass, ref groundTransform, groundShape);


			CollisionShape boxShape = new BoxShape(new Vector3(0.2f, 0.2f, 0.2f));

			Matrix boxTransform = Matrix.Identity;
			boxTransform.Translation = new Vector3(0, 3, 0);


			LocalCreateRigidBody(1.0f, boxTransform, boxShape);



		}


		static void Main(string[] args)
		{
			using (LargeMeshDemo game = new LargeMeshDemo())
			{
				game.Run();
			}
		}

static int numTriangles = 14;
static Vector3[] vertices = {
	new Vector3(1.78321f ,1.78321f ,-1.783479f),
	new Vector3(1.78321f ,-1.78321f ,-1.783479f),
	new Vector3(-1.78321f ,-1.78321f ,-1.783479f),
	new Vector3(-1.78321f ,1.78321f ,-1.783479f),
	new Vector3(0.0f ,0.0f ,1.783479f),
	new Vector3(0.0f ,0.0f ,1.783479f),
	new Vector3(0.0f ,0.0f ,1.783479f),
	new Vector3(0.0f ,0.0f ,1.783479f),
	new Vector3(-1.78321f ,-1.78321f ,-1.783479f),
	new Vector3(0.0f ,0.0f ,1.783479f)
};

static int[] indices = {0,1,2,3,0,2,4,5,6,4,6,7,4,2,1,4,1,5,5,1,0,5,0,6,6,0,3,6,3,7,7,3,8,7,8,9,9,8,2,9,2,4};



CollisionShape BuildLargeMesh()
{
	//int vertStride = sizeof(Vector3);
	//int indexStride = 3*sizeof(int);

	int vertStride = 1;
	int indexStride = 3;

	ObjectArray<Vector3> vertexArray = new ObjectArray<Vector3>();
	for (int i = 0; i < vertices.Length; ++i)
	{
		vertexArray.Add(vertices[i]);
	}

	ObjectArray<int> intArray = new ObjectArray<int>();
	for (int i = 0; i < indices.Length; ++i)
	{
		intArray.Add(indices[i]);
	}

	TriangleIndexVertexArray indexVertexArray = new TriangleIndexVertexArray(numTriangles,intArray,indexStride,10,vertexArray,vertStride);
	TriangleMeshShape triangleMesh = new TriangleMeshShape(indexVertexArray);
	return triangleMesh;

}


	}
}
