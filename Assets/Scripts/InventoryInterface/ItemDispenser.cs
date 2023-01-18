using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype {
    [RequireComponent(typeof(SphereCollider))]
    public class ItemDispenser : MonoBehaviour
    {
        public GameObject itemPrefab;
        public List<Item> itemInstances = new List<Item>();
        private SphereCollider dropSpace;
        public int itemLifetime = 5;

        protected virtual void Start()
        {
            dropSpace = GetComponent<SphereCollider>();
            itemInstances = new List<Item>();
            CheckDropSpace();
        }

        void OnTriggerExit(Collider other)
        {
            Debug.Log("OnTriggerExit");
            CheckDropSpace();
        }

        protected int ActiveItems() {
            int count = 0;
            for (int i = 0; i < itemInstances.Count; i++) {
                if (itemInstances[i].enabled) count++;
            }
            return count;
        }

        protected virtual Item CreateItem()
        {
            GameObject obj = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Item item = obj.GetComponent<Item>();
            item.dispenser = this;
            itemInstances.Add(item);
            return item;
        }

        [ContextMenu("Spawn Item")]
        public void SpawnItem()
        {
            Item item = null;
            for (int i = 0; i < itemInstances.Count; i++)
            {
                if (!itemInstances[i].enabled)
                {
                    item = itemInstances[i];
                    break;
                }
            }
            if (item == null) item = CreateItem();
            item.transform.position = transform.position;
            item.enabled = true;
            item.isGrabbed = false;
            item.gameObject.SetActive(true);
            item.ResetLifetime();
        }

        public void DespawnItem(Item item)
        {
            item.isGrabbed = false;
            item.enabled = false;
            item.gameObject.SetActive(false);
            if (EmptyDropSpace()) CheckDropSpace();
        }

        public bool EmptyDropSpace() {
            Collider[] c = Physics.OverlapSphere(transform.position, dropSpace.radius);
            for (int i = 0; i < c.Length; i++)
            {
                if (itemInstances.Contains(c[i].GetComponent<Item>())) return false;
            }
            return true;
        }

        public bool ItemInDropSpace(Item item)
        {
            Collider[] c = Physics.OverlapSphere(transform.position, dropSpace.radius);
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i].GetComponent<Item>() == item) return true;
            }
            return false;
        }

        public Item GetItemInDropSpace()
        {
            Collider[] c = Physics.OverlapSphere(transform.position, dropSpace.radius);
            for (int i = 0; i < c.Length; i++)
            {
                Debug.Log(c[i].name);
                if (itemInstances.Contains(c[i].GetComponent<Item>())) return c[i].GetComponent<Item>();
            }
            return null;
        }

        public virtual void OnItemGrabbed(Item item)
        {
            if (ItemInDropSpace(item)) {
                for (int i = 0; i < itemInstances.Count; i++) {
                    if (itemInstances[i] != item) DespawnItem(itemInstances[i]);
                }
            }
        }

        public virtual void OnItemDropped(Item item)
        {
        }

        protected virtual void CheckDropSpace()
        {
            if (EmptyDropSpace()) SpawnItem();
        }
    }
}