/* 
 * Version History
 * 
 */

/*
 * v1.0.0 (3/20/22)
 * 
 * - Pre-firework balancing
 * - Finished Circler and Firework
 * - Adjusted some movement values
 * 
 * - ToDo: balance fireworks (see FireworkLauncher for more details)
 * 
 */

/* 
 * v1.0.0 (8/11/22)
 * 
 * - Firework balancing
 * - Finished & balanced Chaser and SpawnChasers
 * 
 * - ToDo: FireworkLauncher changes
 * 
 */

/* 
 * v1.0.0 (8/12/22)
 * 
 * - Finished Explosion and SpawnExplosions
 * - Chaser and Explosion balancing
 * 
 * - ToDo: WebGL performance, canvas overhaul + scale, damage sfx
 * 
 */

/*
 * v.1.0.0 (3/5/23)
 * 
 * - Change light bullets into standard sprite bullets (fix performance issue with overlapping lights)
 * 
 */

/*
 * v.1.0.1 (3/6/23)
 * 
 * - Slash and projectile
 * - Resize camera and other objects by x4 in case of floating point errors
 * - Enemy health
 * - Enemy chaser movement pattern
 * 
 */

/*
 * v.1.0.1 (3/7/23)
 * 
 * - Slash and projectile balancing
 *      - Projectile gains more damage the more distance it travels
 *      - Slightly more slash damage
 * - Homing projectile
 * - Explosion attack
 *      - Time between attack is time-dependent (either improve performance or fix coroutine)
 *      
 * - ToDo: ObjectPool on all Instantiates (set active false instead of destroying object)
 *      - Create a separate class to manage all object pools with a shared static instance of the class
 *      - Only convert to ParticleSystem instead of RigidBody2D if still performance issues with many bullets
 * 
 */

/*
 * v.1.0.2 (3/8/23)
 * 
 * - Object pooling system
 * 
 * - ToDo: Physics gives the highest CPU usage -> change RigidBody2D to ParticleSystem, remove object pooling for slash & projectile
 * 
 */


/*
 * v.1.0.3 (12/14/23)
 * 
 * - Singleton for ColorRandomizer (attached to Object Pool as a component)
 * - Set ColorRandomizer script execution order to happen before Object Pooling
 * 
 * - Create material for each bullet color
 * - Changed ColorRandomizer to ParticleColorRandomizer for randomizing material selection
 * 
 * - ToDo: Test particle system emission
 *  
 */

/*
 * v.1.0.3 (12/15/23)
 * 
 * - Create Circler
 * 
 * With help from Maieron's videos:
 * https://www.youtube.com/watch?v=ri3D6BmGSaM
 * https://www.youtube.com/watch?v=46TqkhJu7uA&t=0s
 * And Luigi's Reddit post:
 * https://www.reddit.com/r/Unity2D/comments/uy7m0h/picked_up_unity_about_a_month_ago_to_make_a/
 * 
 * - ToDo: Player damage from collider/trigger, more attack patterns
 *  
 */

/*
 * v.1.0.3 (12/16/23)
 * 
 * - Damage calculations
 * - Change from renderer to texture sheet (because WebGL particles had a weird black background)
 * - Particle destruction
 * - Particle render behind objects (set order in layer to -1)
 * - Boss health bar
 * 
 * With help from xqtr123 (how to use particle trigger):
 * https://discussions.unity.com/t/gettriggerparticles-always-returns-zero-in-onparticletrigger/246262/2
 * And ifurkend (how to delete particles on collision):
 * https://forum.unity.com/threads/destroy-single-particles.490654/
 * And Simon (how to set slider to 0):
 * https://forum.unity.com/threads/how-to-make-a-slider-empty.297347/
 * And BMo (how to do a health bar):
 * https://www.youtube.com/watch?v=_lREXfAMUcE
 * 
 * - ToDo: Show boss health increasing to show that the bar represents boss health
 * 
 */

/*
 * v.1.0.3 (12/18/23)
 * 
 * - Arrow attack and particle trail
 * - Change script execution order so that Arrow can configure properties of ArrowParticles
 * 
 */

/*
 * v.1.0.3 (12/20/23)
 * 
 * - Start on boss slash attack
 * - Quadrant calculation
 * 
 * - ToDo: Object pool boss slash projectile and finish the slash attack
 * 
 */

/*
 * v.1.0.3 (12/20/23)
 * 
 * - Almost finish slash attack
 * 
 * - ToDo: Object pool arrow and boss slash projectile, make slash start slowly and gradually increase in speed
 * 
 */

/*
 * v.1.0.3 (12/21/23)
 * 
 * - Finish slash attack (add collider and implement speed changes)
 * - Object pool arrow and boss slash projectile
 * - Regroup attack scripts
 * 
 * - ToDo: Explosion attack, add arrow reset instead of destroy
 * 
 */

/*
 * v.1.0.3 (12/25/23)
 * 
 * - Arrow reset
 * - Set ClosestQuadrant script execution order to occur first
 * - Boss attack pattern
 * 
 * - ToDo: Explosion attack
 * 
 */

/*
 * v.1.0.3 (12/26/23)
 * 
 * - Explosion projectiles
 * 
 * - ToDo: Explosion boss movement and object pooling
 * 
 */

/*
 * v.1.0.3 (12/27/23)
 * 
 * - Object pooled explosion
 * - Start boss movement
 * 
 * - ToDo: Finish explosion boss movement
 *         Fix using https://stackoverflow.com/questions/36485185/smoothdamp-only-works-in-update
 * 
 */

/*
 * v.1.0.3 (12/28/23)
 * 
 * - Boss movement for BossParticleExplosion
 * 
 * - ToDo: Limit where the boss can move based on player location
 * 
 */

/*
 * v.1.0.3 (12/29/23)
 * 
 * - Finish particle explosion
 * 
 * - ToDo: Scythe attack
 * 
 */

/*
 * v.1.0.3 (12/30/23)
 * 
 * - Scythe art
 * - Scythe path test using Bezier curves
 * 
 * With help from Alexander Zotov's Bezier curve video:
 * https://www.youtube.com/watch?v=11ofnLOE8pw
 * 
 * - ToDo: Scythe attack
 * 
 */