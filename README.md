# GalagoMod

**Galago** (also known as bush babies) are a type of animal that usually lives inside central Africa, and around the west and southeast regions of Asia. They are the sister group to the *Lorisidae,* which are known as a series of *strepsirrhine primates*... 

According to wikipedia, that is. This galago, however, seems to have taken up the form of a pathfinder extension. Nature is truly wild.


# Description
GalagoMod is a pathfinder extension with a high emphasis on customization; whenever it be through a customizable trace or setting a forkbombs unique effect, Galago is a sweet treat to any pathfinder modders out there that don't like having to follow a strict set of rules for each action; most of the time, you can just plop them down in your action command without much modification. What you do from there is up to you!


# Installation
To install this mod, like any other pathfinder extension, create a "/plugins" folder in your main extension, then put the DLL you downloaded in there. Simple as that! **Make SURE [Pathfinder](https://github.com/Arkhist/Hacknet-Pathfinder) is installed, or this extension won't work!!!**


## Can i use this?
Yes! Infact, I appreciate you doing so! This should work without Labryrinths.



# Features

**Note that NONE OF THESE ACTIONS ARE DELAYABLE.** Create a seperate addconditionalactions if you wish to delay these.

## Eulogy Trace

`<StartEulogy Seconds="num"/>`

Eulogy Trace, developed by ~~Verdant Rays~~ your fictional extension company, is an action that  starts immediately as it is called, with "Seconds" signifying how long it'll take until it reaches 0. Once it reaches 0, it will crash the users computer, or initiate a trace if they have the corrosponding flags.

But what makes Eulogy unique is how it works; disconnecting from a node still triggers the fail condition. To fully get rid of Eulogy, you have to delete the logs off of a computer. Removing all of the logs grants the flag "eulogyDone".

Look at the example Eulogy folder for more details.

## LirazBomb
`<StartLirazBomb />`

LirazBomb is a special type of forkbomb that incentivizes ps killing it. Due to this, it is a much slower forkbomb. Once LirazBomb is done executing (without being ps killed) then the flag "LbombDone" is given.

Why is it called LirazBomb? Well, I know of a little extension called Artif-

## Copy Protection
`<DisableCopyFile /> |
<EnableCopyFile />`

Simple in concept, simple in word. First one disables copying files from computers, second one enables it back. Great use with Eulogy, or if you simply don't want the person accessing something earlier than intended. Note that this command is universal, meaning it doesn't apply to a single computer.