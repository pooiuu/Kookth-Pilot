using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NoParameter ();
public delegate void CollisionProcess (Collision2D col, Player p);
public delegate void PlayerProcess (Player p);
public delegate void IntParameter (int i);
public delegate void EngineIdentity (EngineDirection d);

public class EventPool {
}
