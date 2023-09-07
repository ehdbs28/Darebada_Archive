using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoController : MonoBehaviour
{
    public List<Deco> decos = new List<Deco>();
    public Deco decoObject;
    public List<Transform> decoPositions = new List<Transform>();

    public void AddDeco(DecoVisualSO visual)
    {
        Deco deco = Instantiate(decoObject);
        deco.transform.parent = transform;
        deco.transform.localPosition = decoPositions[Random.Range(0, decoPositions.Count)].localPosition;
        decos.Add(deco);
        deco.Initialize(visual);
        AquariumManager.Instance.decoCnt++;

    }

}
