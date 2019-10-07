using UnityEngine;

public class LookDirection : MonoBehaviour
{
    // 今回は見たいオブジェクト(向く方向)
    public GameObject target;

    // 見る位置が変わるのを検知
    private Vector3 beforeTargetPosition;

    // 自分の位置
    public Transform startPosition;

    // Sleapで使用する補完率
    float step = 0;

    // 向くスピード(秒速)
    float speed = 0.01f;

    // Xキーを押したかフラグ
    private bool xKeyFlg;

    void Start()
    {
        startPosition = transform;
        beforeTargetPosition = target.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Y軸を回転軸にして１０度ずつ回転");
            this.transform.Rotate(new Vector3(0, 1, 0), 10); // Y軸を回転軸にして１０度ずつ回転
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("ワールド座標基準で、現在の角度にRotation[1,1,1]を加算する");
            this.transform.Rotate(1.0f, 1.0f, 1.0f, Space.World); // ワールド座標基準で、現在の角度にRotation[1,1,1]を加算する
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Y軸を回転軸にして90度回転した方向へゲームオブジェクトを回転");
            var angles = new Vector3(0f, 90f, 0f); // 角度を指定
            this.transform.rotation = Quaternion.Euler(angles); // 指定した角度に向かせる

            // 以下同じ結果
//            var angles = new Vector3(0f, 90f, 0f);    // 角度を指定
//            var direction = Quaternion.Euler(angles) * Vector3.forward;
//            this.transform.rotation = Quaternion.LookRotation(direction);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("現在のゲームオブジェクトの角度にY軸を回転軸にして45度回転を追加する");
            var angles = new Vector3(0f, 45f, 0f);
            this.transform.rotation = Quaternion.Euler(angles) * this.transform.rotation;

//            // 以下同じ結果
//            var angles = new Vector3(0f, 45f, 0f);
//            var direction = Quaternion.Euler(angles) * Vector3.forward;
//            this.transform.rotation = Quaternion.LookRotation(direction) * this.transform.rotation;
//            this.transform.rotation = Quaternion.Euler(angles) * this.transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("ゲームオブジェクトをY軸を回転軸にして45度回転させる");
            Quaternion q1 = Quaternion.Euler(0f, 0f, 0f); // 回転A
            Quaternion q2 = Quaternion.Euler(0f, 90f, 0f); // 回転B
            this.transform.rotation = Quaternion.Lerp(q1, q2, 0.5f); // 線形補間　Y軸を回転軸にして45度回転した位置まで回転する。(0～90度の補完率0.5だから45度）
        }

        // Xキーを押している最中にターゲットのカプセル(Capsule)にキューブ(Cube)がゆっくりと向いていく。
        if (Input.GetKeyDown(KeyCode.X))
        {
            xKeyFlg = true;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            xKeyFlg = false;
        }

        if (xKeyFlg) RotationSlow();
    }

    /// <summary>
    /// ゆっくり回転させる関数
    /// </summary>
    private void RotationSlow()
    {
        //秒速に直す計算
        step += speed * Time.deltaTime;

        // offsetがゼロの場合は、transform.forwardの方向がcupsuleに向くが、offsetを入れることで、ゲームオブジェクトの左側面をcupsuleに向かせるなどが可能になる。
        Quaternion offset = Quaternion.AngleAxis(90.0f, Vector3.up); // Z軸方向からのオフセット角度を指定　（Y軸を回転軸にして90度回転させるQuaternionを取得）
        Quaternion targetDir = Quaternion.LookRotation((target.transform.position - startPosition.position).normalized) * offset; // 2点間の角度差にオフセット角度も加算

        transform.rotation = Quaternion.Slerp(startPosition.rotation, targetDir, step); // 現在のゲームオブジェクトの角度から目標の角度までゆっくりと回転させる
        // 注意：この処理の場合はstepが0.1fくらいでtargetDirに向く。理由は、targetDirは毎フレームでtargetとstartPositionとの角度差を再計算しているため
        
        // ポジションが途中で変わるのを検出して再び向き始める
        if (beforeTargetPosition != target.transform.position)
        {
            step = 0;
            startPosition = this.transform;
        }

        beforeTargetPosition = target.transform.position;
    }
}