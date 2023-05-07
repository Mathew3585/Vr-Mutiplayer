using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetWorkPlayer : MonoBehaviour
{

    public Transform Head;
    public Transform LeftHand;
    public Transform RightHand;

    public Animator leftHandAnimator;
    public Animator righrHandAnimator;

    private PhotonView photonView;

    private Transform HeadRig;
    private Transform LeftRig;
    private Transform RightRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        XROrigin rig = FindObjectOfType<XROrigin>();
        HeadRig = rig.transform.Find("Camera Offset/Main Camera");
        LeftRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        RightRig = rig.transform.Find("Camera Offset/RightHand Controller");

        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            MapPostion(Head, HeadRig);
            MapPostion(LeftHand, LeftRig);
            MapPostion(RightHand, RightRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), righrHandAnimator);
        }
    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }


    void MapPostion(Transform target, Transform rigtransform) 
    {

        target.position = rigtransform.position;
        target.rotation = rigtransform.rotation;

    }

}
