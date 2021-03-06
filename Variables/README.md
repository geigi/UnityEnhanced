# Variables

This package allows abstraction of variables in MonoBehaviours so that you can link them to a shared asset file dynamically. It is based on [Scriptable Objects](https://docs.unity3d.com/Manual/class-ScriptableObject.html). Based on the genuine work of Ryan Hipple, I intruduced generic types to extend the system by other useful types.

Includes the following types:
- float
- int
- string
- bool
- GameObject
- Transform

and is easily extensible for new types!

## Usage

In your MonoBehaviour or ScriptableObject script, instead of writing:

``` cs
public bool myBool;
```

you write:

``` cs
public BoolReference myBool;
```

You can use that BoolReference like a normal bool.

``` cs
if(myBool)
{
  ...  
}
```

In the inspector you can choose between using a constant or a variable to back up that field. 

![](https://s18.postimg.org/ugfxc3sd5/image.png)

Constants work like normal bools while a variable is defined by a ScriptableObject saved in an asset file. You create Variables by right clicking in your Project Window > Create > Variables > Bool Variable.

The Scriptable Object exists globally and can be referenced from within multiple scenes. This is useful for shared data between multiple scripts and scenes.

## Acknowledgement

This is based on the genius work of Ryan Hipple:

- [https://github.com/roboryantron/Unite2017](https://github.com/roboryantron/Unite2017)
- [Youtube - Unite Austin 2017 - Game Architecture with Scriptable Objects](https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=3244s) *Perfectly describes usage of this system.*
