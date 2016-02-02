using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public abstract class Scene
    {
        public List<GameObject> SceneObjects { get; protected set; }

        public string Name { get; private set; }

        public Scene(string name) //DO NOT make scene-related objects here, that's what InitScene() is for!
        {
            Console.WriteLine("Scene '" + name + "' created. (not yet initialized!)");
            Name = name;
            SceneObjects = new List<GameObject>();
        }

        public GameObject MakeSceneObject(GameObject obj) //only used to track all objects related to this scene in the List SceneObjects, doesn't do much more.
        {
            if (SceneObjects == null)
                SceneObjects = new List<GameObject>();

            SceneObjects.Add(obj);
            obj.baseScene = this;
            obj.destroyed = false;

            return obj;
        }

        public void DeleteSceneObject(GameObject obj)
        {
            SceneObjects.Remove(obj);
            obj.Destroy();
        }

        public void UpdateScene()
        {
            Update();
        }

        public virtual void Clean() //Destroy all objects in the scene! Called when a scene is destroyed to make place for a new one. Override if additional cleaning is needed.
        {
            while (SceneObjects.Count > 0)
                DeleteSceneObject(SceneObjects[0]);
        }

        public virtual void Update() { } //Main loop for the scene. Make stuff happen here!
        public abstract void InitScene(); //Called to set up scene opbjects and initialize whatever the scene is meant to do.
    }
}
