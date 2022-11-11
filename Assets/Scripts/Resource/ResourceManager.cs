using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resource
{
    public enum ResourceType
    {
        Food,
        Water,
        Wood,
        Stone,
        Iron,
        Gold
    }

    public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
    {
        [SerializeField] private List<Resource> Resources = new List<Resource>();

        [SerializeField] private GameObject ResourceSourcePrefab;

        private List<ResourceSourceAgent> ResourceSources = new List<ResourceSourceAgent>();

        public void Awake()
        {
            foreach (Resource resource in Resources)
            {
                SpawnResourceSources(resource, Vector2.zero, 10);
            }
        }

        public void SpawnResourceSources(Resource resource, Vector2 position, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var randomPosition = position + new Vector2(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(-25, 25));

                GameObject resourceSource = Instantiate(ResourceSourcePrefab, randomPosition, Quaternion.identity);
                resourceSource.transform.parent = transform;

                resourceSource.GetComponent<ResourceSourceAgent>().ResourceSource = resource;

                ResourceSources.Add(resourceSource.GetComponent<ResourceSourceAgent>());
            }
        }

        public void AddResourceSource(ResourceSourceAgent resourceSource)
        {
            ResourceSources.Add(resourceSource);
        }

        public ResourceSourceAgent RemoveResourceSource(Resource resource)
        {
            throw new System.NotImplementedException();
        }

        public ResourceSourceAgent GetClosestResourceSourceOfType(ResourceType resource, Vector2 position)
        {
            ResourceSourceAgent closest = null;

            foreach (ResourceSourceAgent resourceSource in ResourceSources)
            {
                if (resourceSource.ResourceSource.ResourceType == resource)
                {
                    if (closest == null)
                    {
                        closest = resourceSource;
                    }
                    else
                    {
                        if (Vector3.Distance(resourceSource.transform.position, position) < Vector3.Distance(closest.transform.position, position))
                        {
                            closest = resourceSource;
                        }
                    }
                }
            }

            return closest;
        }
    }
}
