using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Pour pouvoir voir le montant d'entrée dans l'unité sans autoriser d'autres scripts à y accéder.
   [SerializeField] private float moveSpeed; // [SerializeField] utilisé pour assurer que la variable reste privée.
   [SerializeField] private float walkSpeed;
   [SerializeField] private float runSpeed; 


  // Variables pour la direction et le contrôleur.

   private Vector3 moveDirection;
   private Vector3 velocity;


   [SerializeField] private bool isGrounded; 
   [SerializeField] private float groundCheckDistance;
   [SerializeField] private LayerMask groundMask;
   [SerializeField] private float gravity;

   [SerializeField] private float jumpHeight;


   public CharacterController controller;
   public Animator anim;
   public AudioSource source;
   public AudioClip clip;
   public int coins;
   


   //Pour que le contrôleur de variable puisse accéder au contrôleur de caractère dans l'unité.
   private void Start() //Méthose appelée à chaque debut du frame.
   {
       controller = GetComponent<CharacterController>(); //Sert à utiliser le composant qui se trouve dans l'inspecteur du caractère 'CharacterController'.
       anim = GetComponent<Animator>();
   }


   
   private void Update() //Méthode utilisée à chaqur frame.
   {
        Move();

        
        if(Input.GetKey(KeyCode.LeftAlt)) //La touche 'alt' qui se situe au coté gauche du clavier.
        {
            source.PlayOneShot(clip); //Jouer l'audio aprés avoir clicker sur L'input en quesction.
            Attack(); //Méthode qui déclenche l'attaque 1.

        
        }
        else if(Input.GetKey(KeyCode.RightAlt)) //La touche 'alt' qui se situe au coté droit du clavier.
        {
            source.PlayOneShot(clip);//Jouer l'audio aprés avoir clicker sur L'input en quesction.
            Attack2();//Méthode qui déclenche l'attaque 2.

            
        }
   }

// Créer un mouvement et mettre en œuvre
   private void Move()
   {

       //PLaces sphere on player feet model, to check if we are touching the ground or not.
       isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask); //Vérifier si le player touche le sol.

       if(isGrounded && velocity.y < 0) 
       {
           velocity.y = -2f;
       }

        float moveZ = Input.GetAxis("Vertical");  //Les touches 'haut' et 'bas' du clavier.
       
        


        moveDirection = new Vector3(0, 0, moveZ); //Déclarer 'moveDirection' comme vecteur de 3 dimension qui sert a faire bouger le player.
        moveDirection = transform.TransformDirection(moveDirection);


        
    

        if(isGrounded)
        {
            if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) // Vérifie si on ne click pas sur la touche 'ShiftGauche' pour déclencher la méthode walk.
            {
                Walk(); //Déclencher la méthode Walk pour faire marcher le player.
            }
            else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) //Vérifie si on click sur la touche 'ShiftGauche' pour déclencher la méthode run.
            {
                Run(); //Déclencher la méthode Run pour faire courir le player.
            }
            else if(moveDirection == Vector3.zero) //Vérifie si le vecteur de direction et égal à 0 pour déclencher la méthode Idle.
            {
                idle();  //Déclencher la méthode Idle pour ne pas faire bouger le player.      
            }


            

            if(Input.GetKeyDown(KeyCode.Space)) //La touche 'space' qui se situe en bas du clavier
            {
                Jump(); //Sert à faire sauter le player.
            }

        }


        moveDirection *= moveSpeed; // Attacher moveDirection a la variable moveSpeed pour faire bouger le player ou non.

        controller.Move(moveDirection * Time.deltaTime); //Vérifie move direction et le temps nécessaire pour faire bouger le joueur.

        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);
   }

   private void idle() // vérifie si le joueur ne bouge pas.
   {
      anim.SetFloat("Speed", 0); //Exécuter l'animation Idle dans l'animator controller.
   }
   
   private void Walk() // vérifie si le joueur marche.
   {
       moveSpeed = walkSpeed;
       anim.SetFloat("Speed", 0.5f); //Exécuter l'animation Walk dans l'animator controller.
   }

   private void Run() // vérifie si le joueur court.
   {
       moveSpeed = runSpeed;
       anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime); //Exécuter l'animation Run dans l'animator controller.
   }

   private void Jump() // vérifie si le joueur saute.
   {
       velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); //le player saute .
   }

   
   private void Attack() // vérifie si le joueur va attaquer.
   {
       anim.SetTrigger("Attack"); //Exécuter l'animation Attack dans l'animator controller.
   }

   private void Attack2() // vérifie si le joueur va attaquer mais avec une autre animation.
   {
       anim.SetTrigger("Attack2"); //Exécuter l'animation Attack2 dans l'animator controller.
   }

   public void Death() // vérifie si le joueur meurt.
   {
       anim.SetTrigger("Death"); //Exécuter l'animation Death dans l'animator controller.
   }

   public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Coin")

        {
            Debug.Log("Coin collected!");
            coins = coins + 1;
           // Col.gameObject.SetActive(false);
            Destroy(Col.gameObject);
        }
    }
   
   

}
