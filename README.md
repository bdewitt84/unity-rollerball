# CS 410 - Game Development
## Author
Brett DeWitt
## Description

[60] Download and install Unity 6000.0.44f1 Personal (including the preselected targets plus “WebGL Build Support” and any other desired platform support) either from the Unity Hub (recommended) or directly from here:

https://unity.com/releases/editor/qa/lts-releases

Links to an external site.

This specific version was chosen for its stability and because it’s compatible with the tutorials for Assignments 1 and 2.  It will also be critical that we’re all using the same version for the term project.  We’ll need the WebGL support for later in the term, so might as well install it now!

Next, do all of the steps here except "Adding AI Navigation" (you may do this if you'd like - consider it optional):

https://learn.unity.com/project/roll-a-ball?uv=6

Links to an external site.

[30] Add a ball “double jump” ability to your project, triggered by the spacebar. Specifically, the ball should be able to jump when in contact with the ground and then jump exactly once more before returning to contact with the ground. There are many ways to implement this, so do some research and have fun!

Feel free to customize the look and feel of your project by adjusting colors, lighting, assets, etc.  Be creative!

[10] Create a GitHub repository for your project using the Unity .gitignore template. Commit (and push) all files/directories in your project directory (the .gitignore will ensure that unnecessary files aren’t included).

Submit a link to your *public* repo for Assignment 1 on Canvas.

## Implementation
For the double jump, I used the built-in callbacks for input event OnJump and collision event OnCollisionEnter. I used two variables to keep track of the jumps: maxJumps and jumpsLeft. The ground in the example code was given a 'ground' tag. When the player enters a collision with a game object which has the ground tag, jumpsLeft is set to maxJumps, resetting the counter. OnJump checks whether jumpsLeft is greater than zero, and if so decrements jumpsLeft and applies force.

```c#
    void OnJump(InputValue jumpValue)
    {
        if (jumpsLeft > 0)
        {
            Vector3 jumpVec = new Vector3(0.0f, jumpForce, 0.0f);

            rb.AddForce(jumpVec, ForceMode.Impulse);

            jumpsLeft -= 1;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = maxJumps;
        }
    }
```

