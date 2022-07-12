using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_FolderBehaviour : Storage_FileBehaviour
{    
    override public void DeletePrefab()
    {
        StartCoroutine(SpawnThenDie());
    }

    private IEnumerator SpawnThenDie()
    {
        yield return new WaitForSeconds(0);

        for (int i = 0; i < _fileInfolder; i++)
        {
            _manager.Spawn(transform);
        }
        _manager.HandleFileDeath();
        Destroy(this.gameObject);
    }
}
