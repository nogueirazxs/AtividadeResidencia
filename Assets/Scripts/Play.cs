using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Play : MonoBehaviour
{
    InputAction moveAction, JumpAction; // Variável do tipo InputAction para receber as ações configuradas no ActionMap;
    public float velocidade = 5f; // controla a velocidade de movimento
    public float forcaPulo = 5f; // controla a força do pulo

    private Rigidbody2D rb; // variavel para pegar o corpo do personagem
    private SpriteRenderer sp; // variavel para pegar a propriedade de flip do personagem

    public int pulos = 0; // variavel para contabilizar os pulos
    public TextMeshProUGUI texto; // variavel para receber o texto que atualizará a quantidade de pulos

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // pega os componente do personagem
        sp = GetComponent<SpriteRenderer>();
        moveAction = InputSystem.actions.FindAction("Move"); // procura a ação Move e Jump configurada no ActionMap
        JumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        float move = moveAction.ReadValue<Vector2>().x; // recebe o valor do movimento do tipo Vector2
        rb.linearVelocity = new Vector2(move * velocidade, rb.linearVelocity.y); // move de acordo com o valor do movimento multiplicado a velocidade
                                                                                 // preservando o eixo y;

        if (move != 0) // se houver valor diferente de zero no movimento
        {
            sp.flipX = move < 0; // flipa o personagem de acordo com o valor
        }

        if (JumpAction.WasPressedThisFrame()) // se apertou na tecla configurada referente a ação de Pulo
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaPulo); // aplica uma força pra cima e preserva o eixo x;
            pulos += 1; // incrementa na quantidade de pulos
            texto.text = "Pulos: " + pulos; // atualiza o texto
        }
        
    }

    void OnTriggerEnter2D(Collider2D col) // verificação de sobreposição de uma colisão
    {
        if (col.CompareTag("Espinho")) // se for um objeto com a tag Espinho
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // cena reinicia.
        }
    }
}
