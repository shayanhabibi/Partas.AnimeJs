# Partas.AnimeJs

> [!WARNING]
> Work in progress.

![Animation.gif](Animation.gif)

## Intention

Fable bindings for the phenomenal [v4+ AnimeJs animation library](https://animejs.com/).

> Check out their v4 landing page


## ComputationExpressions

Computations that have their operations defined on the type directly should show them in auto completion.

![img.png](./img.png)

Otherwise, you can `dot` the computation builder and filter the completions by `Op`. This should show all defined custom operations.

![img_1.png](./img_1.png)

When viewing the operations doc in full, the remarks describe:

- The operation name
- Number of args acceptable
- Different type signatures for each arg count

![img_2.png](./img_2.png)

When viewing the operation docs in shorthand, the doc will list:

- The operation name
- Type signature for that particular member

![img_3.png](./img_3.png)

## Progress

The bindings are essentially done. There are some dependencies on Partas.Solid that will have to be cleaned up though.

~~I'm creating the wrapper before I clean it all up.~~

The Record/Union version was absolutely painful to work with. JS being the dynamic thing it is, it isn't a surprise.

I've taken the Feliz style for the bindings/wrapper, as this provides the most flexibility.

If you want to 'create' an attribute/key val pair for a prop list that doesn't exist, you can open the `Partas.AnimeJs.FSharp.UnsafeOperators` and use the `==<` to map any key,value pair to the required type. It is supposed to look off putting so it deters usage.

## Update 17/6

Some issues with feliz style and AnimeJs

- Pollution of namespace/autocompletion due to including all native targetable style properties/attributes.

To address this, we separate these props/attributes into a qualified name access like so:

```f#
// instead of
Animation.backgroundColor // no longer available member
// we do
Animation.Style.backgroundColor
```

---

Different groups of common properties fall under types which output a generic interface implementation type.

```f#
// things like delay, loop, reverse, alternate
type IPlaybackProp = interface end

[<Interface>]
type Playback =
    static member inline reverse(): #IPlaybackProp = ...

// The playback props are valid animation
// props/options.

type IAnimationProp =
    inherit IPlaybackProp

[<Interface>]
type Animation =
    inherit Playback
// the 'reverse' member is available via
// the Animation type interface, and will
// be accepted anywhere IPlaybackProp
// is (including IAnimationProp).
```

---

```fsharp
// Example input

AnimeJs.createSpring [ // Only accepts ISpringProp list
    Spring.damping 5.
    "fd" ==< "2" // will create force create a key value pair
] |> ignore


TimeLabel "Chock" // Explicit typed timeline labels
|> ignore


let x = AnimeJs.animate (unbox null) [
    // IAnimationProp list
    Animation.duration 500
    Animation.alternate true
    // IAnimationProp inherits IAnimatableProp
    Animation.Style.border "1px solid red"
    // IAnimationProp inherits IAnimatablePropObj<IKeyframeProp>
    Animation.Style.backgroundColor [
        Keyframe.duration 500
        Keyframe.to' "sd"
    ]
    // IAnimationProp inherits IKeyframeValue
    Animation.keyframes [
        KeyframeValue.create [
            // IKeyframeValue includes all keyframe props except to/from
            Keyframe.Style.accentColor "accent"
            Keyframe.duration 500
        ]
    ]
    Animation.ease Ease.InOutSine
]

```

```js
// Example output

createSpring({
    damping: 5,
    fd: "2",
});

"Chock";

export const x = animate(defaultOf(), {
    duration: 500,
    alternate: true,
    border: "1px solid red",
    backgroundColor: {
        duration: 500,
        to: "sd",
    },
    keyframes: [{
        accentColor: "accent",
        duration: 500,
    }],
    ease: "inOutSine",
});
```
