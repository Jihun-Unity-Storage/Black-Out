using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    public Image healthBarImage; // Unity UI Image�� ����Ͽ� ü�� ǥ��
    private Monster monster; // ���Ϳ� ���� ����

    public void SetMonster(Monster monster)
    {
        // ü�� �� ��ũ��Ʈ�� ���͸� ����
        this.monster = monster;
        //Debug.Log(monster.name);
    }

    private void Update()
    {
        if (monster != null)
        {
            //Debug.Log(monster.name);
            // ������ ���� ü�� �� �ִ� ü�� ��������
            float currentHealth = monster.GetCurrentHealth();
            float maxHealth = monster.GetMaxHealth();
            //Debug.Log(currentHealth);

            // ü���� FillAmount�� ���� (0���� 1 ������ ������ ����ȭ)
            healthBarImage.fillAmount = currentHealth / maxHealth;
        }
    }
}
