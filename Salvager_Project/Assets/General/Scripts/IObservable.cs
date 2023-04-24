using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    public void ObserverSuscribe();

    public void ObserverUnsuscribe();
}
