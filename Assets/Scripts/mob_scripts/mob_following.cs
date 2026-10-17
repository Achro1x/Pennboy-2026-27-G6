using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_following : MonoBehaviour
{

    public float speed;
    private mob_targetchooser target_chooser;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target_chooser = GetComponent<mob_targetchooser>();
    }

    // Update is called once per frame
    void Update()
    {
/*        if (target_chooser == null)
        return;

        Vector3? target =
            target_chooser.FindNearestRedSquare();

        if (!target.HasValue)
            return;

        Vector3 target_position = target.Value;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target_position,
            speed * Time.deltaTime
        );*/
    }
}
