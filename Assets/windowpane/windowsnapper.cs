using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class windowsnapper : MonoBehaviour
{

    Vector3 wallspot1;
    Vector3 wallspot2;
    Vector3 wallspot3;
    Vector3 wallspot4;

    float contact_threshold = 0.25f;

    public bool snapped1 = false;
    public bool snapped2 = false;
    public bool snapped3 = false;
    public bool snapped4 = false;

    // Start is called before the first frame update
    void Start()
    {
        wallspot1 = GameObject.Find("Canvas/WallSpot1").transform.position;
        wallspot2 = GameObject.Find("Canvas/WallSpot2").transform.position;
        wallspot3 = GameObject.Find("Canvas/WallSpot3").transform.position;
        wallspot4 = GameObject.Find("Canvas/WallSpot4").transform.position;
    }

    public GameObject selectedObject;
    Vector3 offset;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics2D.OverlapPoint(mousePosition))
        {
            Collider2D[] results = Physics2D.OverlapPointAll(mousePosition);
            Collider2D highestCollider = GetHighestObject(results);
            selectedObject = highestCollider.transform.gameObject;
            offset = selectedObject.transform.position - mousePosition;
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            Vector3 cur_position = selectedObject.transform.position;
            if ((cur_position - wallspot1).magnitude < contact_threshold && !snapped1)
            {
                snapped1 = true;
                selectedObject.transform.position = wallspot1;
                Destroy(selectedObject.GetComponent<BoxCollider2D>());
            }
            else if ((cur_position - wallspot2).magnitude < contact_threshold && !snapped2)
            {
                snapped2 = true;
                selectedObject.transform.position = wallspot2;
                Destroy(selectedObject.GetComponent<BoxCollider2D>());
            }
            else if ((cur_position - wallspot3).magnitude < contact_threshold && !snapped3)
            {
                snapped3 = true;
                selectedObject.transform.position = wallspot3;
                Destroy(selectedObject.GetComponent<BoxCollider2D>());

            }
            else if ((cur_position - wallspot4).magnitude < contact_threshold && !snapped4)
            {
                snapped4 = true;
                selectedObject.transform.position = wallspot4;
                Destroy(selectedObject.GetComponent<BoxCollider2D>());
            }
            selectedObject = null;
        }

        if (snapped1 && snapped2 && snapped3 && snapped4)
        {
            StartCoroutine("winGame");
        }

    }

    Collider2D GetHighestObject(Collider2D[] results)
    {
        int highestValue = 0;
        Collider2D highestObject = results[0];
        foreach (Collider2D col in results)
        {
            Renderer ren = col.gameObject.GetComponent<Renderer>();
            if (ren && ren.sortingOrder > highestValue)
            {
                highestValue = ren.sortingOrder;
                highestObject = col;
            }
        }
        return highestObject;
    }

    IEnumerator winGame()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("End", LoadSceneMode.Single);
    }

    IEnumerator FadeOut()
    {
        Image image = GameObject.Find("Canvas/Wall").GetComponent<Image>();
        float FadeRate = 0.2f;
        float targetAlpha = 0f;
        Color curColor = image.color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            image.color = curColor;
            yield return null;
        }
    }
}
