# Partas.AnimeJs

> [!WARNING]
> Work in progress.

![Animation.gif](Animation.gif)

## Intention

Fable bindings for the phenomenal [v4+ AnimeJs animation library](https://animejs.com/).

> Check out their v4 landing page


## Progress

The bindings are essentially done. There are some dependencies on Partas.Solid that will have to be cleaned up though.

~~I'm creating the wrapper before I clean it all up.~~

The Record/Union version was absolutely painful to work with. JS being the dynamic thing it is, it isn't a surprise.

I've taken the Feliz style for the bindings/wrapper, as this provides the most flexibility.

If you want to 'create' an attribute/key val pair for a prop list that doesn't exist, you can open the `Partas.AnimeJs.FSharp.UnsafeOperators` and use the `==<` to map any key,value pair to the required type. It is supposed to look off putting so it deters usage.
