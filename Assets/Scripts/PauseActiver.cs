using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseActiver : MonoBehaviour
{
    public Transform pp;
    private void OnEnable()
    {
        pp.gameObject.SetActive(false);
    }
    public void makeP()
    {

        pp.gameObject.SetActive(! pp.gameObject.activeInHierarchy);
    }
}
