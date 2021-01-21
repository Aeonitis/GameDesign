using UnityEngine;

namespace Extension.Movement
{
    /**
     * Move Transform in 
     *
     * Notes:
     * - Local space with transform.*, Global space with Vector3.* e.g. transform.forward replaces Vector3.forward
     * - Negative right gives us left, Negative Up is Down, and Negative forward gives us backward ;)
     */
    public static class TransformMovementExtension
    {
        public static void SetToPosition(this Transform transform, Vector3 positionVector3) =>
            transform.position = positionVector3;

        // Local Axes (X,Y,Z) controls ---------------------------------------------------

        public static void TurnLeftOnLocalAxes(this Transform transform, float rotationSpeed) =>
            transform.Rotate(Time.deltaTime * rotationSpeed * -transform.up);

        public static void TurnRightOnLocalAxes(this Transform transform, float rotationSpeed) =>
            transform.Rotate(Time.deltaTime * rotationSpeed * transform.up);

        public static void MoveRightOnLocalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * transform.right;

        public static void MoveLeftOnLocalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * -transform.right;

        public static void MoveForwardOnLocalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * transform.forward;

        public static void MoveBackwardOnLocalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * -transform.forward;


        // Global Axes (X,Y,Z) controls --------------------------------------------------------

        public static void TurnLeftOnGlobalAxes(this Transform transform, float rotationSpeed) =>
            transform.Rotate(Time.deltaTime * rotationSpeed * Vector3.down);

        public static void TurnRightOnGlobalAxes(this Transform transform, float rotationSpeed) =>
            transform.Rotate(Time.deltaTime * rotationSpeed * Vector3.up);


        public static void MoveRightOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * Vector3.right;

        public static void MoveLeftOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * Vector3.left;

        public static void MoveForwardOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * Vector3.forward;

        public static void MoveBackwardOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Time.deltaTime * movementSpeed * Vector3.back;


        // Glitch (Teleport) Movement on Global Axes (X,Y,Z) controls ---------------------------------
        public static void GlitchMoveRightOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Vector3.right;

        public static void GlitchMoveLeftOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Vector3.left;

        public static void GlitchMoveForwardOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Vector3.forward;

        public static void GlitchMoveBackwardOnGlobalAxes(this Transform transform, float movementSpeed) =>
            transform.position += Vector3.back;
    }
}