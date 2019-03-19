using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Serialization;
using System;

namespace Kosmos.UI.Transition
{
    [AddComponentMenu("FluxUI/Flux Animation")]
    public class FluxAnimation : MonoBehaviour 
    {
        public enum VCTimeMode
        {
            ScaledTime,
            UnscaledTime
        }

        public enum VCAnimationDirection
        {
            Forward,
            Reverse
        }

        public enum VCAnimationState
        {
            AnimStarted,
            AnimFinished
        }

        public enum VCAnimationMethod
        {
            AnimFunction,
            AnimCurve
        }

        public enum VCColorComponentType
        {
            None,
            SpriteRenderer,
            Renderer,
            Image,
            Text
        }

        /// <summary>
        /// Enumeration of all easing equations.
        /// </summary>
        public enum Equations
        {
            None,
            Linear,
            QuadEaseOut, QuadEaseIn, QuadEaseInOut, QuadEaseOutIn,
            ExpoEaseOut, ExpoEaseIn, ExpoEaseInOut, ExpoEaseOutIn,
            CubicEaseOut, CubicEaseIn, CubicEaseInOut, CubicEaseOutIn,
            QuartEaseOut, QuartEaseIn, QuartEaseInOut, QuartEaseOutIn,
            QuintEaseOut, QuintEaseIn, QuintEaseInOut, QuintEaseOutIn,
            CircEaseOut, CircEaseIn, CircEaseInOut, CircEaseOutIn,
            SineEaseOut, SineEaseIn, SineEaseInOut, SineEaseOutIn,
            ElasticEaseOut, ElasticEaseIn, ElasticEaseInOut, ElasticEaseOutIn,
            BounceEaseOut, BounceEaseIn, BounceEaseInOut, BounceEaseOutIn,
            BackEaseOut, BackEaseIn, BackEaseInOut, BackEaseOutIn
        }


        [System.Serializable]
        public class VCAnimationInfo
        {
            public VCAnimationMethod AnimMethod;
            public AnimationCurve CurveX;
            public AnimationCurve CurveY;
            public AnimationCurve CurveZ;
            public Equations XEquation;
            public Equations YEquation;
            public Equations ZEquation;
            public float Duration;
            public float Delay;
            public Vector3 Start;
            public Vector3 End;

            public bool ShowInspector = false;

            public VCAnimationInfo()
            {
                AnimMethod = VCAnimationMethod.AnimFunction;
                CurveX = AnimationCurve.EaseInOut(0, 0, 1, 1);
                CurveY = AnimationCurve.EaseInOut(0, 0, 1, 1);
                CurveZ = AnimationCurve.EaseInOut(0, 0, 1, 1);
                XEquation = Equations.None;
                YEquation = Equations.None;
                Duration = 1;
                Delay = 0;
            }
        }

        [System.Serializable]
        public class VCAlphaInfo
        {
            public float Duration;
            public float Delay;
            public float Start;
            public float End;

            public bool ShowInspector = false;

            public VCAlphaInfo()
            {
                Duration = 1;
                Delay = 0;
                Start = 1;
                End = 1;
            }
        }

        [System.Serializable]
        public class VCColorInfo
        {
            public VCAnimationMethod AnimMethod;
            public AnimationCurve Curve;
            public Equations Equation;

            public float Duration;
            public float Delay;
            public Color Start;
            public Color End;

            public bool ShowInspector = false;

            public VCColorInfo()
            {
                Curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
                Equation = Equations.None;
                Duration = 1;
                Delay = 0;
                Start = Color.white;
                End = Color.white;
            }
        }

        [System.Serializable]
        public class FluxAnimationData
        {
            public VCAnimationInfo OpenAnimation;
            public VCAnimationInfo CloseAnimation;
            public bool Enable = false;
            public bool Autoclose = false;
            public float AutocloseDelay = 0;
            public bool Autoplay = false;
            public bool Loop = false;

            public bool ShowInspector = false;
        }

        [System.Serializable]
        public class VCAlphaData
        {
            public VCAlphaInfo OpenAnimation;
            public VCAlphaInfo CloseAnimation;
            public bool Enable = false;
            public bool Autoclose = false;
            public float AutocloseDelay = 0;
            public bool Autoplay = false;
            public bool Loop = false;

            public bool ShowInspector = false;
        }

        [System.Serializable]
        public class FluxColorData
        {
            public VCColorInfo OpenAnimation;
            public VCColorInfo CloseAnimation;
            public bool Enable = false;
            public bool Autoclose = false;
            public float AutocloseDelay = 0;
            public bool Autoplay = false;
            public bool Loop = false;

            public bool ShowInspector = false;
        }

        public struct EquationFunctionData
        {
            public EquationFunction XOpenFunction;
            public EquationFunction YOpenFunction;
            public EquationFunction ZOpenFunction;

            public EquationFunction XCloseFunction;
            public EquationFunction YCloseFunction;
            public EquationFunction ZCloseFunction;
        }

        public delegate float EquationFunction(float t, float b, float c, float d);

        [System.Serializable]
        public class AnimNotificationEvent : UnityEvent { }

        // Transform
        public FluxAnimationData Movement;
        public FluxAnimationData Rotation;
        public FluxAnimationData Scale;

        // Alpha
        public VCAlphaData Alpha;

        // Color
        public FluxColorData ColorData;

        public VCTimeMode TimeMode;

        [FormerlySerializedAs("onOpenStart")]
        [SerializeField]
        public AnimNotificationEvent onOpenStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onOpenEnd")]
        [SerializeField]
        public AnimNotificationEvent onOpenEnd = new AnimNotificationEvent();

        [FormerlySerializedAs("onCloseStart")]
        [SerializeField]
        public AnimNotificationEvent onCloseStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onCloseEnd")]
        [SerializeField]
        public AnimNotificationEvent onCloseEnd = new AnimNotificationEvent();

        ///  Movement
        [FormerlySerializedAs("onMovementOpenStart")]
        [SerializeField]
        public AnimNotificationEvent onMovementOpenStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onMovementOpenEnd")]
        [SerializeField]
        public AnimNotificationEvent onMovementOpenEnd = new AnimNotificationEvent();

        [FormerlySerializedAs("onMovementCloseStart")]
        [SerializeField]
        public AnimNotificationEvent onMovementCloseStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onMovementCloseEnd")]
        [SerializeField]
        public AnimNotificationEvent onMovementCloseEnd = new AnimNotificationEvent();

        ///  Rotation
        [FormerlySerializedAs("onRotationOpenStart")]
        [SerializeField]
        public AnimNotificationEvent onRotationOpenStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onRotationOpenEnd")]
        [SerializeField]
        public AnimNotificationEvent onRotationOpenEnd = new AnimNotificationEvent();

        [FormerlySerializedAs("onRotationCloseStart")]
        [SerializeField]
        public AnimNotificationEvent onRotationCloseStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onRotationCloseEnd")]
        [SerializeField]
        public AnimNotificationEvent onRotationCloseEnd = new AnimNotificationEvent();

        ///  Scale
        [FormerlySerializedAs("onScaleOpenStart")]
        [SerializeField]
        public AnimNotificationEvent onScaleOpenStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onScaleOpenEnd")]
        [SerializeField]
        public AnimNotificationEvent onScaleOpenEnd = new AnimNotificationEvent();

        [FormerlySerializedAs("onScaleCloseStart")]
        [SerializeField]
        public AnimNotificationEvent onScaleCloseStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onScaleCloseEnd")]
        [SerializeField]
        public AnimNotificationEvent onScaleCloseEnd = new AnimNotificationEvent();

        ///  Alpha
        [FormerlySerializedAs("onAlphaOpenStart")]
        [SerializeField]
        public AnimNotificationEvent onAlphaOpenStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onAlphaOpenEnd")]
        [SerializeField]
        public AnimNotificationEvent onAlphaOpenEnd = new AnimNotificationEvent();

        [FormerlySerializedAs("onAlphaCloseStart")]
        [SerializeField]
        public AnimNotificationEvent onAlphaCloseStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onAlphaCloseEnd")]
        [SerializeField]
        public AnimNotificationEvent onAlphaCloseEnd = new AnimNotificationEvent();

        ///  Color
        [FormerlySerializedAs("onColorOpenStart")]
        [SerializeField]
        public AnimNotificationEvent onColorOpenStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onColorOpenEnd")]
        [SerializeField]
        public AnimNotificationEvent onColorOpenEnd = new AnimNotificationEvent();

        [FormerlySerializedAs("onColorCloseStart")]
        [SerializeField]
        public AnimNotificationEvent onColorCloseStart = new AnimNotificationEvent();

        [FormerlySerializedAs("onColorCloseEnd")]
        [SerializeField]
        public AnimNotificationEvent onColorCloseEnd = new AnimNotificationEvent();

        // Functions
        EquationFunctionData MovementFunctions;
        EquationFunctionData RotationFunctions;
        EquationFunctionData ScaleFunctions;

        VCAnimationState _MovementState = VCAnimationState.AnimFinished;
        VCAnimationState _ScaleState = VCAnimationState.AnimFinished;
        VCAnimationState _RotationState = VCAnimationState.AnimFinished;
        VCAnimationState _AlphaState = VCAnimationState.AnimFinished;
        VCAnimationState _ColorState = VCAnimationState.AnimFinished;

        VCAnimationDirection CurrentAnimDirection = VCAnimationDirection.Forward;

        private IEnumerator MoveFuncRoutine;
        private IEnumerator ScaleFuncRoutine;
        private IEnumerator RotationFuncRoutine;
        private IEnumerator AlphaFuncRoutine;
        private IEnumerator ColorFuncRoutine;

        CanvasGroup canvasGroup;

        // Show Inspector Variables
        public bool ShowAnimations = false;
        public bool ShowEvents = false;
        public bool ShowNormalEvents = false;
        public bool ShowMovementEvents = false;
        public bool ShowRotationEvents = false;
        public bool ShowScaleEvents = false;
        public bool ShowAlphaEvents = false;
        public bool ShowColorEvents = false;

        void Awake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector3 transformRealPosition = new Vector3(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, 0);

            Movement.OpenAnimation.Start += transformRealPosition;
            Movement.CloseAnimation.Start += transformRealPosition;
            Movement.OpenAnimation.End += transformRealPosition;
            Movement.CloseAnimation.End += transformRealPosition;

            canvasGroup = GetComponent<CanvasGroup>();
        
            AssignFunctions();
            InitValues(true);
        }

        // Use this for initialization
        void Start()
        {
            if (Movement.Autoplay && Movement.Enable)
            {
                StartMovementOpenAnimation();
            }

            if (Scale.Autoplay && Scale.Enable)
            {
                StartScaleOpenAnimation();
            }

            if (Rotation.Autoplay && Rotation.Enable)
            {
                StartRotationOpenAnimation();
            }

            if (Alpha.Autoplay && Alpha.Enable)
            {
                StartAlphaOpenAnimation();
            }

            if (ColorData.Autoplay && ColorData.Enable)
            {
                StartColorOpenAnimation();
            }
        }

        void OnEnable()
        {
            if (Movement.Autoplay && Movement.Enable)
            {
                StartMovementOpenAnimation();
            }

            if (Scale.Autoplay && Scale.Enable)
            {
                StartScaleOpenAnimation();
            }

            if (Rotation.Autoplay && Rotation.Enable)
            {
                StartRotationOpenAnimation();
            }

            if (Alpha.Autoplay && Alpha.Enable)
            {
                StartAlphaOpenAnimation();
            }

            if (ColorData.Autoplay && ColorData.Enable)
            {
                StartColorOpenAnimation();
            }
        }

        void InitValues(bool forward)
        {
            RectTransform rectTransform = transform as RectTransform;
            if (forward)
            {
                if (Movement.Enable)
                {
                    if(rectTransform)
                    {
                        rectTransform.anchoredPosition = Movement.OpenAnimation.Start;
                    }
                    else
                    {
                        transform.position = Movement.OpenAnimation.Start;
                    }
                }

                if (Scale.Enable)
                {
                    if (Scale.OpenAnimation.Start != Vector3.zero && Scale.OpenAnimation.End != Vector3.zero)
                    {
                        transform.localScale = Scale.OpenAnimation.Start;
                    }
                }

                if (Rotation.Enable)
                {
                    transform.eulerAngles = Rotation.OpenAnimation.Start;
                }

                if (Alpha.Enable && canvasGroup)
                {
                    canvasGroup.alpha = Alpha.OpenAnimation.Start;
                }

                if (ColorData.Enable && GetComponent<Renderer>())
                {
                    GetComponent<Renderer>().material.color = ColorData.OpenAnimation.Start;
                }
            }
            else
            {
                if (Movement.Enable)
                {
                    if (rectTransform)
                    {
                        rectTransform.anchoredPosition = Movement.CloseAnimation.Start;
                    }
                    else
                    {
                        transform.position = Movement.CloseAnimation.Start;
                    }
                }

                if (Scale.Enable)
                {
                    if (Scale.CloseAnimation.Start != Vector3.zero && Scale.CloseAnimation.End != Vector3.zero)
                    {
                        transform.localScale = Scale.CloseAnimation.Start;
                    }
                }

                if (Rotation.Enable)
                {
                    transform.eulerAngles = Rotation.CloseAnimation.Start;
                }

                if (Alpha.Enable && canvasGroup)
                {
                    canvasGroup.alpha = Alpha.CloseAnimation.Start;
                }

                if (ColorData.Enable && GetComponent<Renderer>())
                {
                    GetComponent<Renderer>().material.color = ColorData.CloseAnimation.Start;
                }
            }
        }

        void AssignFunctions()
        {
            // Open Functions
            MovementFunctions.XOpenFunction = GetEquationFunction(Movement.OpenAnimation.XEquation);
            MovementFunctions.YOpenFunction = GetEquationFunction(Movement.OpenAnimation.YEquation);
            MovementFunctions.ZOpenFunction = GetEquationFunction(Movement.OpenAnimation.ZEquation);

            RotationFunctions.XOpenFunction = GetEquationFunction(Rotation.OpenAnimation.XEquation);
            RotationFunctions.YOpenFunction = GetEquationFunction(Rotation.OpenAnimation.YEquation);
            RotationFunctions.ZOpenFunction = GetEquationFunction(Rotation.OpenAnimation.ZEquation);

            ScaleFunctions.XOpenFunction = GetEquationFunction(Scale.OpenAnimation.XEquation);
            ScaleFunctions.YOpenFunction = GetEquationFunction(Scale.OpenAnimation.YEquation);
            ScaleFunctions.ZOpenFunction = GetEquationFunction(Scale.OpenAnimation.ZEquation);

            // Close Functions
            MovementFunctions.XCloseFunction = GetEquationFunction(Movement.CloseAnimation.XEquation);
            MovementFunctions.YCloseFunction = GetEquationFunction(Movement.CloseAnimation.YEquation);
            MovementFunctions.ZCloseFunction = GetEquationFunction(Movement.CloseAnimation.ZEquation);

            RotationFunctions.XCloseFunction = GetEquationFunction(Rotation.CloseAnimation.XEquation);
            RotationFunctions.YCloseFunction = GetEquationFunction(Rotation.CloseAnimation.YEquation);
            RotationFunctions.ZCloseFunction = GetEquationFunction(Rotation.CloseAnimation.ZEquation);

            ScaleFunctions.XCloseFunction = GetEquationFunction(Scale.CloseAnimation.XEquation);
            ScaleFunctions.YCloseFunction = GetEquationFunction(Scale.CloseAnimation.YEquation);
            ScaleFunctions.ZCloseFunction = GetEquationFunction(Scale.CloseAnimation.ZEquation);
        }

        /// <summary>
        /// Starts All the Opening Animations
        /// </summary>
        public void StartOpeningAnimation()
        {
            InitValues(true);

            StopMovementAnimation();
            StopScaleAnimation();
            StopRotationAnimation();
            StopAlphaAnimation();
            StopColorAnimation();

            StartMovementOpenAnimation();
            StartScaleOpenAnimation();
            StartRotationOpenAnimation();
            StartAlphaOpenAnimation();
            StartColorOpenAnimation();

            onOpenStart.Invoke();
            CurrentAnimDirection = VCAnimationDirection.Forward;

            if (!Movement.Enable && !Scale.Enable && !Rotation.Enable && !Alpha.Enable)
            {
                StopAnimation();
                OnAnimationFinished();
            }
        }

        /// <summary>
        /// Starts all the closing animations
        /// </summary>
        public void StartClosingAnimation()
        {
            InitValues(false);

            StopMovementAnimation();
            StopScaleAnimation();
            StopRotationAnimation();
            StopAlphaAnimation();
            StopColorAnimation();

            StartMovementCloseAnimation();
            StartScaleCloseAnimation();
            StartRotationCloseAnimation();
            StartAlphaCloseAnimation();
            StartColorCloseAnimation();

            onCloseStart.Invoke();
            CurrentAnimDirection = VCAnimationDirection.Reverse;

            if (!Movement.Enable && !Scale.Enable && !Rotation.Enable && !Alpha.Enable)
            {
                StopAnimation();
                OnAnimationFinished();
            }
        }

        /// <summary>
        /// Stops All the Open Animations
        /// </summary>
        public void StopAnimation()
        {
            StopMovementAnimation();
            StopScaleAnimation();
            StopRotationAnimation();
            StopAlphaAnimation();
            StopColorAnimation();

            _MovementState = VCAnimationState.AnimFinished;
            _ScaleState = VCAnimationState.AnimFinished;
            _RotationState = VCAnimationState.AnimFinished;
            _AlphaState = VCAnimationState.AnimFinished;
            _ColorState = VCAnimationState.AnimFinished;

            if (CurrentAnimDirection == VCAnimationDirection.Forward)
            {
                onOpenEnd.Invoke();
            }
            else
            {
                onCloseEnd.Invoke();
            }
        }

        /// <summary>
        /// Calls Automatically when each Animation is finished
        /// </summary>
        public void OnAnimationFinished()
        {
            if (_MovementState == VCAnimationState.AnimFinished &&
                _ScaleState == VCAnimationState.AnimFinished &&
                _RotationState == VCAnimationState.AnimFinished &&
                _AlphaState == VCAnimationState.AnimFinished &&
                _ColorState == VCAnimationState.AnimFinished)
            {
                StopAnimation();
            }
        }

        // Functions for Each Transformation Control

        #region Open Functions

        /// <summary>
        /// Starts Movement Open Animation
        /// </summary>
        public void StartMovementOpenAnimation()
        {
            if (MoveFuncRoutine != null)
            {
                StopCoroutine(MoveFuncRoutine);
            }

            MoveFuncRoutine = MoveFunc(true);
            StartCoroutine(MoveFuncRoutine);
        }

        /// <summary>
        /// Starts Scale Open Animation
        /// </summary>
        public void StartScaleOpenAnimation()
        {
            if (ScaleFuncRoutine != null)
            {
                StopCoroutine(ScaleFuncRoutine);
            }

            ScaleFuncRoutine = ScaleFunc(true);
            StartCoroutine(ScaleFuncRoutine);
        }

        /// <summary>
        /// Starts Rotation Open Animation
        /// </summary>
        public void StartRotationOpenAnimation()
        {
            if (RotationFuncRoutine != null)
            {
                StopCoroutine(RotationFuncRoutine);
            }

            RotationFuncRoutine = RotationFunc(true);
            StartCoroutine(RotationFuncRoutine);
        }

        /// <summary>
        /// Starts Alpha Open Animation
        /// </summary>
        public void StartAlphaOpenAnimation()
        {
            if (AlphaFuncRoutine != null)
            {
                StopCoroutine(AlphaFuncRoutine);
            }

            AlphaFuncRoutine = AlphaFunc(true);
            StartCoroutine(AlphaFuncRoutine);
        }

        /// <summary>
        /// Starts Color Open Animation
        /// </summary>
        public void StartColorOpenAnimation()
        {
            if (ColorFuncRoutine != null)
            {
                StopCoroutine(ColorFuncRoutine);
            }

            ColorFuncRoutine = ColorFunc(true);
            StartCoroutine(ColorFuncRoutine);
        }

        #endregion

        #region Close Functions

        /// <summary>
        /// Starts Movement Close Animation
        /// </summary>
        public void StartMovementCloseAnimation()
        {
            if (MoveFuncRoutine != null)
            {
                StopCoroutine(MoveFuncRoutine);
            }

            MoveFuncRoutine = MoveFunc(false);
            StartCoroutine(MoveFuncRoutine);
        }

        /// <summary>
        /// Starts Scale Close Animation
        /// </summary>
        public void StartScaleCloseAnimation()
        {
            if (ScaleFuncRoutine != null)
            {
                StopCoroutine(ScaleFuncRoutine);
            }

            ScaleFuncRoutine = ScaleFunc(false);
            StartCoroutine(ScaleFuncRoutine);
        }

        /// <summary>
        /// Starts Rotation Close Animation
        /// </summary>
        public void StartRotationCloseAnimation()
        {
            if (RotationFuncRoutine != null)
            {
                StopCoroutine(RotationFuncRoutine);
            }

            RotationFuncRoutine = RotationFunc(false);
            StartCoroutine(RotationFuncRoutine);
        }

        /// <summary>
        /// Starts Alpha Close Animation
        /// </summary>
        public void StartAlphaCloseAnimation()
        {
            if (AlphaFuncRoutine != null)
            {
                StopCoroutine(AlphaFuncRoutine);
            }

            AlphaFuncRoutine = AlphaFunc(false);
            StartCoroutine(AlphaFuncRoutine);
        }

        /// <summary>
        /// Starts Color Close Animation
        /// </summary>
        public void StartColorCloseAnimation()
        {
            if (ColorFuncRoutine != null)
            {
                StopCoroutine(ColorFuncRoutine);
            }

            ColorFuncRoutine = ColorFunc(false);
            StartCoroutine(ColorFuncRoutine);
        }

        #endregion

        #region Stop Functions

        /// <summary>
        /// Stop Movement Animation
        /// </summary>
        public void StopMovementAnimation()
        {
            if (MoveFuncRoutine != null)
            {
                StopCoroutine(MoveFuncRoutine);
            }
        }

        /// <summary>
        /// Stop Scale Animation
        /// </summary>
        public void StopScaleAnimation()
        {
            if (ScaleFuncRoutine != null)
            {
                StopCoroutine(ScaleFuncRoutine);
            }
        }

        /// <summary>
        /// Stop Rotation Animation
        /// </summary>
        public void StopRotationAnimation()
        {
            if (RotationFuncRoutine != null)
            {
                StopCoroutine(RotationFuncRoutine);
            }
        }

        /// <summary>
        /// Stop Alpha Animation
        /// </summary>
        public void StopAlphaAnimation()
        {
            if (AlphaFuncRoutine != null)
            {
                StopCoroutine(AlphaFuncRoutine);
            }
        }

        /// <summary>
        /// Stop Color Animation
        /// </summary>
        public void StopColorAnimation()
        {
            if (ColorFuncRoutine != null)
            {
                StopCoroutine(ColorFuncRoutine);
            }
        }

        #endregion

        ////////////////////////////////////////////////////

        /// <summary>
        /// Movement Enumerator Function
        /// </summary>
        /// <param name="forward">True if it's an open animation, False if its a Close Animation</param>
        /// <returns>The IEnumerator</returns>
        public IEnumerator MoveFunc(bool forward)
        {
            if (!Movement.Enable)
            {
                yield break;
            }

            _MovementState = VCAnimationState.AnimStarted;

            while (true)
            {
                if (forward)
                {
                    onMovementOpenStart.Invoke();
                }
                else
                {
                    onMovementCloseStart.Invoke();
                }

                float startTime = Time.time;
                float time = 0;
                bool bDelayed = false;

                float Duration = forward ? Movement.OpenAnimation.Duration : Movement.CloseAnimation.Duration;
                float Delay = forward ? Movement.OpenAnimation.Delay : Movement.CloseAnimation.Delay;

                if (!bDelayed && Delay > 0)
                {
                    bDelayed = true;
                    yield return StartCoroutine(WaitForRealSeconds(Delay));
                }

                RectTransform rectTransform = transform as RectTransform;

                while (true)
                {
                    time = Time.time - startTime;

                    if(rectTransform)
                    {
                        rectTransform.anchoredPosition = GetAnimationValue(MovementFunctions, Movement, time, forward);
                    }
                    else
                    {
                        transform.position = GetAnimationValue(MovementFunctions, Movement, time, forward);
                    }

                    if (time < Duration)
                    {
                        yield return StartCoroutine(WaitForRealSeconds(.016f));
                    }
                    else
                    {
                        if (rectTransform)
                        {
                            rectTransform.anchoredPosition = GetAnimationValue(MovementFunctions, Movement, Duration, forward);
                        }
                        else
                        {
                            transform.position = GetAnimationValue(MovementFunctions, Movement, Duration, forward);
                        }
                        break;
                    }
                }

                if (forward)
                {
                    onMovementOpenEnd.Invoke();
                }
                else
                {
                    onMovementCloseEnd.Invoke();
                }

                if (forward)
                {
                    if (Movement.Autoclose)
                    {
                        forward = false;
                        yield return StartCoroutine(WaitForRealSeconds(Movement.AutocloseDelay));
                    }
                    else
                    {
                        if (!Movement.Loop)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!Movement.Loop)
                    {
                        break;
                    }

                    forward = true;
                }
            }

            _MovementState = VCAnimationState.AnimFinished;
            OnAnimationFinished();
        }

        /// <summary>
        /// Scale Enumerator Function
        /// </summary>
        /// <param name="forward">True if it's an open animation, False if its a Close Animation</param>
        /// <returns>The IEnumerator</returns>
        public IEnumerator ScaleFunc(bool forward)
        {
            if (!Scale.Enable)
            {
                yield break;
            }

            _ScaleState = VCAnimationState.AnimStarted;

            while (true)
            {
                if (forward)
                {
                    onScaleOpenStart.Invoke();
                }
                else
                {
                    onScaleCloseStart.Invoke();
                }

                float startTime = Time.time;
                float time = 0;
                bool bDelayed = false;

                float Duration = forward ? Scale.OpenAnimation.Duration : Scale.CloseAnimation.Duration;
                float Delay = forward ? Scale.OpenAnimation.Delay : Scale.CloseAnimation.Delay;

                if (!bDelayed && Delay > 0)
                {
                    bDelayed = true;
                    yield return StartCoroutine(WaitForRealSeconds(Delay));
                }

                Vector3 Start = forward ? Scale.OpenAnimation.Start : Scale.CloseAnimation.Start;
                Vector3 End = forward ? Scale.OpenAnimation.End : Scale.CloseAnimation.End;

                while (true)
                {
                    time = Time.time - startTime;

                    if (Start != Vector3.zero && End != Vector3.zero)
                    {
                        transform.localScale = GetAnimationValue(ScaleFunctions, Scale, time, forward);
                        if (time < Duration)
                        {
                            yield return StartCoroutine(WaitForRealSeconds(.016f));
                        }
                        else
                        {
                            transform.localScale = GetAnimationValue(ScaleFunctions, Scale, Duration, forward);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (forward)
                {
                    onScaleOpenEnd.Invoke();
                }
                else
                {
                    onScaleCloseEnd.Invoke();
                }

                if (forward)
                {
                    if (Scale.Autoclose)
                    {
                        forward = false;
                        yield return StartCoroutine(WaitForRealSeconds(Scale.AutocloseDelay));
                    }
                    else
                    {
                        if(!Scale.Loop)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!Scale.Loop)
                    {
                        break;
                    }

                    forward = true;
                }
            }

            _ScaleState = VCAnimationState.AnimFinished;
            OnAnimationFinished();

        }

        /// <summary>
        /// Rotation Enumerator Function
        /// </summary>
        /// <param name="forward">True if it's an open animation, False if its a Close Animation</param>
        /// <returns>The IEnumerator</returns>
        public IEnumerator RotationFunc(bool forward)
        {
            if (!Rotation.Enable)
            {
                yield break;
            }

            _RotationState = VCAnimationState.AnimStarted;

            while (true)
            {
                if (forward)
                {
                    onRotationOpenStart.Invoke();
                }
                else
                {
                    onRotationCloseStart.Invoke();
                }

                float startTime = Time.time;
                float time = 0;
                bool bDelayed = false;

                float Duration = forward ? Rotation.OpenAnimation.Duration : Rotation.CloseAnimation.Duration;
                float Delay = forward ? Rotation.OpenAnimation.Delay : Rotation.CloseAnimation.Delay;

                if (!bDelayed && Delay > 0)
                {
                    bDelayed = true;
                    yield return StartCoroutine(WaitForRealSeconds(Delay));
                }

                while (true)
                {
                    time = Time.time - startTime;
                    transform.eulerAngles = GetAnimationValue(RotationFunctions, Rotation, time, forward);
                    if (time < Duration)
                    {
                        yield return StartCoroutine(WaitForRealSeconds(.016f));
                    }
                    else
                    {
                        transform.eulerAngles = GetAnimationValue(RotationFunctions, Rotation, Duration, forward);
                        break;
                    }
                }

                if (forward)
                {
                    onRotationOpenEnd.Invoke();
                }
                else
                {
                    onRotationCloseEnd.Invoke();
                }

                if (forward)
                {
                    if (Rotation.Autoclose)
                    {
                        forward = false;
                        yield return StartCoroutine(WaitForRealSeconds(Rotation.AutocloseDelay));
                    }
                    else
                    {
                        if (!Rotation.Loop)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!Rotation.Loop)
                    {
                        break;
                    }

                    forward = true;
                }
            }

            _RotationState = VCAnimationState.AnimFinished;
            OnAnimationFinished();
        }

        /// <summary>
        /// Alpha Enumerator Function
        /// </summary>
        /// <param name="forward">True if it's an open animation, False if its a Close Animation</param>
        /// <returns>The IEnumerator</returns>
        public IEnumerator AlphaFunc(bool forward)
        {
            if (!Alpha.Enable || !canvasGroup)
            {
                yield break;
            }

            _AlphaState = VCAnimationState.AnimStarted;

            while (true)
            {
                if (forward)
                {
                    onAlphaOpenStart.Invoke();
                }
                else
                {
                    onAlphaCloseStart.Invoke();
                }

                float startTime = Time.time;
                float time = 0;
                bool bDelayed = false;

                float Duration = forward ? Alpha.OpenAnimation.Duration : Alpha.CloseAnimation.Duration;
                float Delay = forward ? Alpha.OpenAnimation.Delay : Alpha.CloseAnimation.Delay;
                float Start = forward ? Alpha.OpenAnimation.Start : Alpha.CloseAnimation.Start;
                float End = forward ? Alpha.OpenAnimation.End : Alpha.CloseAnimation.End;

                if (!bDelayed && Delay > 0)
                {
                    bDelayed = true;
                    yield return StartCoroutine(WaitForRealSeconds(Delay));
                }

                while (true)
                {
                    time = Time.time - startTime;
                    canvasGroup.alpha = Lerp(Start, End, (float)time / Duration);
                    if (time < Duration)
                    {
                        yield return StartCoroutine(WaitForRealSeconds(.016f));
                    }
                    else
                    {
                        canvasGroup.alpha = Lerp(Start, End, 1);
                        break;
                    }
                }

                if (forward)
                {
                    onAlphaOpenEnd.Invoke();
                }
                else
                {
                    onAlphaCloseEnd.Invoke();
                }

                if (forward)
                {
                    if (Alpha.Autoclose)
                    {
                        forward = false;
                        yield return StartCoroutine(WaitForRealSeconds(Alpha.AutocloseDelay));
                    }
                    else
                    {
                        if (!Alpha.Loop)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!Alpha.Loop)
                    {
                        break;
                    }

                    forward = true;
                }
            }

            _AlphaState = VCAnimationState.AnimFinished;
            OnAnimationFinished();
        }

        /// <summary>
        /// Color Enumerator Function
        /// </summary>
        /// <param name="forward">True if it's an open animation, False if its a Close Animation</param>
        /// <returns>The IEnumerator</returns>
        public IEnumerator ColorFunc(bool forward)
        {
            if (!ColorData.Enable)
            {
                yield break;
            }

            _ColorState = VCAnimationState.AnimStarted;

            VCColorComponentType ComponentType = VCColorComponentType.None;

            if(GetComponent<SpriteRenderer>())
            {
                ComponentType = VCColorComponentType.SpriteRenderer;
            }
            else if (GetComponent<Renderer>())
            {
                ComponentType = VCColorComponentType.Renderer;
            }
            else if (GetComponent<Image>())
            {
                ComponentType = VCColorComponentType.Image;
            }
            else if (GetComponent<Text>())
            {
                ComponentType = VCColorComponentType.Text;
            }
            else
            {
                yield break;
            }

            while (true)
            {
                if (forward)
                {
                    onColorOpenStart.Invoke();
                }
                else
                {
                    onColorCloseStart.Invoke();
                }

                float startTime = Time.time;
                float time = 0;
                bool bDelayed = false;

                float Duration = forward ? ColorData.OpenAnimation.Duration : ColorData.CloseAnimation.Duration;
                float Delay = forward ? ColorData.OpenAnimation.Delay : ColorData.CloseAnimation.Delay;
                Color Start = forward ? ColorData.OpenAnimation.Start : ColorData.CloseAnimation.Start;
                Color End = forward ? ColorData.OpenAnimation.End : ColorData.CloseAnimation.End;

                if (!bDelayed && Delay > 0)
                {
                    bDelayed = true;
                    yield return StartCoroutine(WaitForRealSeconds(Delay));
                }

                while (true)
                {
                    time = Time.time - startTime;
                    Color CurrentColor = GetAnimationValueColor(ColorData, time, forward);
                    switch (ComponentType)
                    {
                        case VCColorComponentType.SpriteRenderer:
                            GetComponent<SpriteRenderer>().color = CurrentColor;
                            break;
                        case VCColorComponentType.Renderer:
                            GetComponent<Renderer>().material.color = CurrentColor;
                            break;
                        case VCColorComponentType.Image:
                            GetComponent<Image>().color = CurrentColor;
                            break;
                        case VCColorComponentType.Text:
                            GetComponent<Text>().color = CurrentColor;
                            break;
                        default:
                            break;
                    }
                    if (time < Duration)
                    {
                        yield return StartCoroutine(WaitForRealSeconds(.016f));
                    }
                    else
                    {
                        CurrentColor = Color.Lerp(Start, End, 1);;
                        switch (ComponentType)
                        {
                            case VCColorComponentType.SpriteRenderer:
                                GetComponent<SpriteRenderer>().color = CurrentColor;
                                break;
                            case VCColorComponentType.Renderer:
                                GetComponent<Renderer>().material.color = CurrentColor;
                                break;
                            case VCColorComponentType.Image:
                                GetComponent<Image>().color = CurrentColor;
                                break;
                            case VCColorComponentType.Text:
                                GetComponent<Text>().color = CurrentColor;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }

                if (forward)
                {
                    onColorOpenEnd.Invoke();
                }
                else
                {
                    onColorCloseEnd.Invoke();
                }

                if (forward)
                {
                    if (ColorData.Autoclose)
                    {
                        forward = false;
                        yield return StartCoroutine(WaitForRealSeconds(ColorData.AutocloseDelay));
                    }
                    else
                    {
                        if (!ColorData.Loop)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    if (!ColorData.Loop)
                    {
                        break;
                    }

                    forward = true;
                }
            }

            _ColorState = VCAnimationState.AnimFinished;
            OnAnimationFinished();
        }

        /// <summary>
        /// Lerps a Value from Start to Close by Amount
        /// </summary>
        /// <param name="Start">Start Value</param>
        /// <param name="Close">Close Value</param>
        /// <param name="amount">Amount Value</param>
        /// <returns></returns>
        private float Lerp(float Start, float Close, float amount)
        {
            return Start + (Close - Start) * amount;
        }

        /// <summary>
        /// Wait for Seconds Based on the Time Mode
        /// </summary>
        /// <param name="time">Time to wait for</param>
        /// <returns></returns>
        public IEnumerator WaitForRealSeconds(float time)
        {
            switch (TimeMode)
            {
                case VCTimeMode.ScaledTime:
                    {
                        float start = Time.timeSinceLevelLoad;
                        while (Time.timeSinceLevelLoad < start + time)
                        {
                            yield return null;
                        }
                    }
                    break;
                case VCTimeMode.UnscaledTime:
                    {
                        float start = Time.realtimeSinceStartup;
                        while (Time.realtimeSinceStartup < start + time)
                        {
                            yield return null;
                        }
                    }
                    break;
                default:
                    {
                        float start = Time.timeSinceLevelLoad;
                        while (Time.timeSinceLevelLoad < start + time)
                        {
                            yield return null;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Returns the delta time based on Current Time Mode
        /// </summary>
        /// <returns>Returns DeltaTime</returns>
        public float GetDeltaTime()
        {
            switch (TimeMode)
            {
                case VCTimeMode.ScaledTime:
                    return Time.deltaTime;
                case VCTimeMode.UnscaledTime:
                    return Mathf.Clamp(Time.unscaledDeltaTime, 0, 0.2f);
                default:
                    return Time.deltaTime;
            }
        }

        /// <summary>
        /// Returns the Animation Value for a Function and Data Given
        /// </summary>
        /// <param name="Functions">Equation</param>
        /// <param name="Data">Animation Data</param>
        /// <param name="TimeStep">Timestep</param>
        /// <param name="forward">True if it's an Open animation, False otherwise</param>
        /// <returns>Vector Value of the Animation</returns>
        private Vector3 GetAnimationValue(EquationFunctionData Functions, FluxAnimationData Data, float TimeStep, bool forward = true)
        {
            Vector3 Start = forward ? Data.OpenAnimation.Start : Data.CloseAnimation.Start;
            Vector3 End = forward ? Data.OpenAnimation.End : Data.CloseAnimation.End;

            VCAnimationMethod Method = forward ? Data.OpenAnimation.AnimMethod : Data.CloseAnimation.AnimMethod;
            float Duration = forward ? Data.OpenAnimation.Duration : Data.CloseAnimation.Duration;

            Vector3 ResultVector = Vector3.zero;
            if (Method == VCAnimationMethod.AnimCurve)
            {
                AnimationCurve CurveX = forward ? Data.OpenAnimation.CurveX : Data.CloseAnimation.CurveX;
                AnimationCurve CurveY = forward ? Data.OpenAnimation.CurveY : Data.CloseAnimation.CurveY;
                AnimationCurve CurveZ = forward ? Data.OpenAnimation.CurveZ : Data.CloseAnimation.CurveZ;

                ResultVector.x = Lerp(Start.x, End.x, CurveX.Evaluate(TimeStep / Duration));
                ResultVector.y = Lerp(Start.y, End.y, CurveY.Evaluate(TimeStep / Duration));
                ResultVector.z = Lerp(Start.z, End.z, CurveZ.Evaluate(TimeStep / Duration));
            }
            else
            {
                EquationFunction XFunctionData = forward ? Functions.XOpenFunction : Functions.XCloseFunction;
                EquationFunction YFunctionData = forward ? Functions.YOpenFunction : Functions.YCloseFunction;
                EquationFunction ZFunctionData = forward ? Functions.ZOpenFunction : Functions.ZCloseFunction;

                if (XFunctionData != null)
                {
                    ResultVector.x = XFunctionData(TimeStep, Start.x, End.x - Start.x, Duration);
                }
                else
                {
                    ResultVector.x = End.x;
                }

                if (YFunctionData != null)
                {
                    ResultVector.y = YFunctionData(TimeStep, Start.y, End.y - Start.y, Duration);
                }
                else
                {
                    ResultVector.y = End.y;
                }

                if (ZFunctionData != null)
                {
                    ResultVector.z = ZFunctionData(TimeStep, Start.z, End.z - Start.z, Duration);
                }
                else
                {
                    ResultVector.z = End.z;
                }
            }

            return ResultVector;
        }

        /// <summary>
        /// Returns the Color for the Data Given
        /// </summary>
        /// <param name="Data">AnimationData</param>
        /// <param name="TimeStep">Timestep</param>
        /// <param name="forward">True if it's an Open animation, False otherwise</param>
        /// <returns>Current Color</returns>
        private Color GetAnimationValueColor(FluxColorData Data, float TimeStep, bool forward = true)
        {
            Color Start = forward ? Data.OpenAnimation.Start : Data.CloseAnimation.Start;
            Color End = forward ? Data.OpenAnimation.End : Data.CloseAnimation.End;

            VCAnimationMethod Method = forward ? Data.OpenAnimation.AnimMethod : Data.CloseAnimation.AnimMethod;
            float Duration = forward ? Data.OpenAnimation.Duration : Data.CloseAnimation.Duration;

            Color Result = Color.black;
            if (Method == VCAnimationMethod.AnimCurve)
            {
                AnimationCurve Curve = forward ? Data.OpenAnimation.Curve : Data.CloseAnimation.Curve;

                Result = Color.Lerp(Start, End, Curve.Evaluate(TimeStep / Duration));
            }
            else
            {
                Equations Equation = forward ? Data.OpenAnimation.Equation : Data.CloseAnimation.Equation;
                EquationFunction Function = GetEquationFunction(Equation);

                if (Function != null)
                {
                    Result.a = Function(TimeStep, Start.a, End.a - Start.a, Duration);
                    Result.r = Function(TimeStep, Start.r, End.r - Start.r, Duration);
                    Result.g = Function(TimeStep, Start.g, End.g - Start.g, Duration);
                    Result.b = Function(TimeStep, Start.b, End.b - Start.b, Duration);
                }
                else
                {
                    Result = Color.black;
                }
            }

            return Result;
        }

        /// <summary>
        /// Returns the Equation Function based on Type
        /// </summary>
        /// <param name="EquationType">Equation Type</param>
        /// <returns>Returns the Equation Function</returns>
        private EquationFunction GetEquationFunction(Equations EquationType)
        {
            EquationFunction ResultFunction = null;
            switch (EquationType)
            {
                case Equations.Linear:
                    ResultFunction = new EquationFunction(Linear);
                    break;
                case Equations.QuadEaseOut:
                    ResultFunction = new EquationFunction(QuadEaseOut);
                    break;
                case Equations.QuadEaseIn:
                    ResultFunction = new EquationFunction(QuadEaseIn);
                    break;
                case Equations.QuadEaseInOut:
                    ResultFunction = new EquationFunction(QuadEaseInOut);
                    break;
                case Equations.QuadEaseOutIn:
                    ResultFunction = new EquationFunction(QuadEaseOutIn);
                    break;
                case Equations.ExpoEaseOut:
                    ResultFunction = new EquationFunction(ExpoEaseOut);
                    break;
                case Equations.ExpoEaseIn:
                    ResultFunction = new EquationFunction(ExpoEaseIn);
                    break;
                case Equations.ExpoEaseInOut:
                    ResultFunction = new EquationFunction(ExpoEaseInOut);
                    break;
                case Equations.ExpoEaseOutIn:
                    ResultFunction = new EquationFunction(ExpoEaseOutIn);
                    break;
                case Equations.CubicEaseOut:
                    ResultFunction = new EquationFunction(CubicEaseOut);
                    break;
                case Equations.CubicEaseIn:
                    ResultFunction = new EquationFunction(CubicEaseIn);
                    break;
                case Equations.CubicEaseInOut:
                    ResultFunction = new EquationFunction(CubicEaseInOut);
                    break;
                case Equations.CubicEaseOutIn:
                    ResultFunction = new EquationFunction(CubicEaseOutIn);
                    break;
                case Equations.QuartEaseOut:
                    ResultFunction = new EquationFunction(QuartEaseOut);
                    break;
                case Equations.QuartEaseIn:
                    ResultFunction = new EquationFunction(QuartEaseIn);
                    break;
                case Equations.QuartEaseInOut:
                    ResultFunction = new EquationFunction(QuartEaseInOut);
                    break;
                case Equations.QuartEaseOutIn:
                    ResultFunction = new EquationFunction(QuartEaseOutIn);
                    break;
                case Equations.QuintEaseOut:
                    ResultFunction = new EquationFunction(QuintEaseOut);
                    break;
                case Equations.QuintEaseIn:
                    ResultFunction = new EquationFunction(QuintEaseIn);
                    break;
                case Equations.QuintEaseInOut:
                    ResultFunction = new EquationFunction(QuintEaseInOut);
                    break;
                case Equations.QuintEaseOutIn:
                    ResultFunction = new EquationFunction(QuintEaseOutIn);
                    break;
                case Equations.CircEaseOut:
                    ResultFunction = new EquationFunction(CircEaseOut);
                    break;
                case Equations.CircEaseIn:
                    ResultFunction = new EquationFunction(CircEaseIn);
                    break;
                case Equations.CircEaseInOut:
                    ResultFunction = new EquationFunction(CircEaseInOut);
                    break;
                case Equations.CircEaseOutIn:
                    ResultFunction = new EquationFunction(CircEaseOutIn);
                    break;
                case Equations.SineEaseOut:
                    ResultFunction = new EquationFunction(SineEaseOut);
                    break;
                case Equations.SineEaseIn:
                    ResultFunction = new EquationFunction(SineEaseIn);
                    break;
                case Equations.SineEaseInOut:
                    ResultFunction = new EquationFunction(SineEaseInOut);
                    break;
                case Equations.SineEaseOutIn:
                    ResultFunction = new EquationFunction(SineEaseOutIn);
                    break;
                case Equations.ElasticEaseOut:
                    ResultFunction = new EquationFunction(ElasticEaseOut);
                    break;
                case Equations.ElasticEaseIn:
                    ResultFunction = new EquationFunction(ElasticEaseIn);
                    break;
                case Equations.ElasticEaseInOut:
                    ResultFunction = new EquationFunction(ElasticEaseInOut);
                    break;
                case Equations.ElasticEaseOutIn:
                    ResultFunction = new EquationFunction(ElasticEaseOutIn);
                    break;
                case Equations.BounceEaseOut:
                    ResultFunction = new EquationFunction(BounceEaseOut);
                    break;
                case Equations.BounceEaseIn:
                    ResultFunction = new EquationFunction(BounceEaseIn);
                    break;
                case Equations.BounceEaseInOut:
                    ResultFunction = new EquationFunction(BounceEaseInOut);
                    break;
                case Equations.BounceEaseOutIn:
                    ResultFunction = new EquationFunction(BounceEaseOutIn);
                    break;
                case Equations.BackEaseOut:
                    ResultFunction = new EquationFunction(BackEaseOut);
                    break;
                case Equations.BackEaseIn:
                    ResultFunction = new EquationFunction(BackEaseIn);
                    break;
                case Equations.BackEaseInOut:
                    ResultFunction = new EquationFunction(BackEaseInOut);
                    break;
                case Equations.BackEaseOutIn:
                    ResultFunction = new EquationFunction(BackEaseOutIn);
                    break;
                default:
                    break;
            }

            return ResultFunction;
        }

        #region Equations

        // These methods are all public to enable reflection in GetCurrentValueCore.

        #region Linear

        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float Linear(float t, float b, float c, float d)
        {
            return c * t / d + b;
        }

        #endregion

        #region Expo

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ExpoEaseOut(float t, float b, float c, float d)
        {
            return (t == d) ? b + c : c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ExpoEaseIn(float t, float b, float c, float d)
        {
            return (t == 0) ? b : c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ExpoEaseInOut(float t, float b, float c, float d)
        {
            if (t == 0)
                return b;

            if (t == d)
                return b + c;

            if ((t /= d / 2) < 1)
                return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;

            return c / 2 * (-Mathf.Pow(2, -10 * --t) + 2) + b;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ExpoEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return ExpoEaseOut(t * 2, b, c / 2, d);

            return ExpoEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Circular

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CircEaseOut(float t, float b, float c, float d)
        {
            return c * Mathf.Sqrt(1 - (t = t / d - 1) * t) + b;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CircEaseIn(float t, float b, float c, float d)
        {
            if (t > 1) { t = 1; }

            return -c * (Mathf.Sqrt(1 - (float)(t /= d) * t) - 1) + b;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CircEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;

            return c / 2 * (Mathf.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CircEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return CircEaseOut(t * 2, b, c / 2, d);

            return CircEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Quad

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuadEaseOut(float t, float b, float c, float d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuadEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t + b;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuadEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t + b;

            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuadEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return QuadEaseOut(t * 2, b, c / 2, d);

            return QuadEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Sine

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float SineEaseOut(float t, float b, float c, float d)
        {
            return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float SineEaseIn(float t, float b, float c, float d)
        {
            return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float SineEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * (Mathf.Sin(Mathf.PI * t / 2)) + b;

            return -c / 2 * (Mathf.Cos(Mathf.PI * --t / 2) - 2) + b;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float SineEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return SineEaseOut(t * 2, b, c / 2, d);

            return SineEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Cubic

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CubicEaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CubicEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t + b;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CubicEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t + b;

            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CubicEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return CubicEaseOut(t * 2, b, c / 2, d);

            return CubicEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Quartic

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuartEaseOut(float t, float b, float c, float d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuartEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t + b;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuartEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t + b;

            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuartEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return QuartEaseOut(t * 2, b, c / 2, d);

            return QuartEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Quintic

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuintEaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuintEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuintEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) < 1)
                return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float QuintEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return QuintEaseOut(t * 2, b, c / 2, d);
            return QuintEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Elastic

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ElasticEaseOut(float t, float b, float c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * .3f;
            float s = p / 4;

            return (c * Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * d - s) * (2f * Mathf.PI) / p) + c + b);
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ElasticEaseIn(float t, float b, float c, float d)
        {
            if ((t /= d) == 1)
                return b + c;

            float p = d * .3f;
            float s = p / 4;

            return -(c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ElasticEaseInOut(float t, float b, float c, float d)
        {
            if ((t /= d / 2) == 2)
                return b + c;

            float p = d * (.3f * 1.5f);
            float s = p / 4;

            if (t < 1)
                return -.5f * (c * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p)) + b;
            return c * Mathf.Pow(2, -10 * (t -= 1)) * Mathf.Sin((t * d - s) * (2 * Mathf.PI) / p) * .5f + c + b;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float ElasticEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return ElasticEaseOut(t * 2, b, c / 2, d);
            return ElasticEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Bounce

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BounceEaseOut(float t, float b, float c, float d)
        {
            if ((t /= d) < (1 / 2.75))
                return c * (7.5625f * t * t) + b;
            else if (t < (2 / 2.75f))
                return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
            else if (t < (2.5 / 2.75))
                return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
            else
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BounceEaseIn(float t, float b, float c, float d)
        {
            return c - BounceEaseOut(d - t, 0, c, d) + b;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BounceEaseInOut(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return BounceEaseIn(t * 2, 0, c, d) * .5f + b;
            else
                return BounceEaseOut(t * 2 - d, 0, c, d) * .5f + c * .5f + b;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BounceEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return BounceEaseOut(t * 2, b, c / 2, d);
            return BounceEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #region Back

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BackEaseOut(float t, float b, float c, float d)
        {
            return c * ((t = t / d - 1) * t * ((1.70158f + 1) * t + 1.70158f) + 1) + b;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BackEaseIn(float t, float b, float c, float d)
        {
            return c * (t /= d) * t * ((1.70158f + 1) * t - 1.70158f) + b;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BackEaseInOut(float t, float b, float c, float d)
        {
            float s = 1.70158f;
            if ((t /= d / 2) < 1)
                return c / 2 * (t * t * (((s *= (1.525f)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525f)) + 1) * t + s) + 2) + b;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float BackEaseOutIn(float t, float b, float c, float d)
        {
            if (t < d / 2)
                return BackEaseOut(t * 2, b, c / 2, d);
            return BackEaseIn((t * 2) - d, b + c / 2, c / 2, d);
        }

        #endregion

        #endregion
    }
}

