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
 * v1.0.1 (8/11/22)
 * 
 * - Firework balancing
 * - Finished & balanced Chaser and SpawnChasers
 * 
 * - ToDo: FireworkLauncher changes
 * 
 */

/* 
 * v1.0.2 (8/12/22)
 * 
 * - Finished Explosion and SpawnExplosions
 * - Chaser and Explosion balancing
 * 
 * - ToDo: WebGL performance, canvas overhaul + scale, damage sfx
 * 
 */

/*
 * v.1.0.3 (3/5/23)
 * 
 * - Change light bullets into standard sprite bullets (fix performance issue with overlapping lights)
 * 
 */

/*
 * v.1.0.4 (3/6/23)
 * 
 * - Slash and projectile
 * - Resize camera and other objects by x4 in case of floating point errors
 * - Enemy health
 * - Enemy chaser movement pattern
 * 
 */

/*
 * v.1.0.5 (3/7/23)
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
 * v.1.0.6 (3/8/23)
 * 
 * - Object pooling system
 * 
 * - ToDo: Physics gives the highest CPU usage -> change RigidBody2D to ParticleSystem, remove object pooling for slash & projectile
 * 
 */