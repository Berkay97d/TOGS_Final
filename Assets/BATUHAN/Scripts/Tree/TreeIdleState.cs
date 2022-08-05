using UnityEngine;

public class TreeIdleState : TreeBaseState
{
    private TreeController _treeController;
    public override void EnterState(TreeStateManager tree)
    {
        Debug.Log("hello from the tree idle state");

        _treeController = tree.GetComponent<TreeController>();
        
        tree.transform.GetChild(1).gameObject.SetActive(true);
        tree.transform.position = new Vector3(tree.transform.position.x, 0, tree.transform.position.z);
    }

    public override void UpdateState(TreeStateManager tree)
    {
    }

    public override void TakeDamage(TreeStateManager tree, int damage)
    {
        Debug.Log(damage + " DAMAGE HAS TAKEN");
        
        if(_treeController.treeHealth > 0)
            _treeController.TakeDamage(damage);
        else 
            tree.SwitchState(tree.deforestState);
    }
}
