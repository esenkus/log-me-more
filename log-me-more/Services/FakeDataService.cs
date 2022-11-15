using System.Collections.Generic;

namespace log_me_more.Services;

public class FakeDataService {
    public const string FAKE_LOG = @"11-14 15:17:21.587  2998  3244 I MicroDetector: Keeping mic open: false
11-14 15:17:21.587  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 15:17:24.670  2648  6292 E memtrack: Couldn't load memtrack module
11-14 15:17:24.671  2648  6292 W android.os.Debug: failed to get memory consumption info: -1
11-14 15:17:24.724  1999  3007 E memtrack: Couldn't load memtrack module
11-14 15:17:24.724  1999  3007 W android.os.Debug: failed to get memory consumption info: -1
11-14 15:17:26.338  2869  6293 E memtrack: Couldn't load memtrack module
11-14 15:17:26.339  2869  6293 W android.os.Debug: failed to get memory consumption info: -1
11-14 15:17:26.590  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 15:17:26.591  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 15:17:26.592  2998  3244 I MicroDetector: Keeping mic open: false
11-14 15:17:26.592  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 15:17:28.226  1999  2038 E memtrack: Couldn't load memtrack module
11-14 15:17:28.226  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 15:17:30.004  3621  6294 E memtrack: Couldn't load memtrack module
11-14 15:17:30.004  3621  6294 W android.os.Debug: failed to get memory consumption info: -1
11-14 15:17:31.594  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 15:17:31.594  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 15:17:31.595  2998  3244 I MicroDetector: Keeping mic open: false
11-14 15:17:31.595  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 15:57:30.618  3621  6416 E memtrack: Couldn't load memtrack module
11-14 15:57:30.618  3621  6416 W android.os.Debug: failed to get memory consumption info: -1
11-14 15:57:32.770  2648  6055 I LogUtils: [Places]: ?: Couldn't find platform key file. [CONTEXT service_id=253 ]
11-14 15:57:32.782  2648  4490 I LogUtils: [Places]: ?: PlacesBleScanner stop() [CONTEXT service_id=253 ]
11-14 15:57:32.783  2648  2648 I BeaconBle: Scan : No clients left, canceling alarm. [CONTEXT service_id=111 ]
11-14 15:57:32.785  2648  2648 I BeaconBle: Scan canceled successfully. [CONTEXT service_id=111 ]
11-14 15:57:32.789  2648  4490 I PlacesLogger: [PlaceInferenceEngine]: [anon] Changed inference mode: 0 [CONTEXT service_id=253 ]
11-14 15:57:32.799  2648  3427 W GCoreFlp: No location to return for getLastLocation()
11-14 15:57:33.794  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 16:00:33.378  1999  2040 I ActivityManager: Start proc 6422:com.google.android.partnersetup/u0a115 for service {com.google.android.partnersetup/com.google.android.partnersetup.InstalledAppJobService}
11-14 16:00:33.412  2648  4429 I NetworkScheduler.Stats: Task com.google.android.inputmethod.latin/ started execution. cause:4 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.421  2518  2518 I FirebaseJobDispatcher: onStartJob(): PeriodicContextualMetrics.
11-14 16:00:33.426  2518  2584 I PContextMetricsRunner: call()
11-14 16:00:33.426  2518  2584 I PContextMetricsRunner: call() : Finished in 0 ms
11-14 16:00:33.427  2518  2518 I FirebaseJobDispatcher: Task: PeriodicContextualMetrics successes.
11-14 16:00:33.447  6422  6422 I id.partnersetu: The ClassLoaderContext is a special shared library.
11-14 16:00:33.454  6422  6422 I id.partnersetu: The ClassLoaderContext is a special shared library.
11-14 16:00:33.456  2648  3512 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.checkin.EventLogService started execution. cause:4 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.466  2648  4429 I NetworkScheduler.Stats: Task com.google.android.inputmethod.latin/ finished executing. cause:4 result: 1 elapsed_millis: 67 uptime_millis: 67 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.476  2869  5852 I Checkin : [EventLogChimeraService] Opted in for usage reporting: false
11-14 16:00:33.477  2869  5852 I Checkin : [EventLogChimeraService] Aggregate from 1668432466418 (log), 1668432466418 (data)
11-14 16:00:33.500  2518  2542 W putmethod.lati: Reducing the number of considered missed Gc histogram windows from 376 to 100
11-14 16:00:33.503  1999  5718 I DropBoxManagerService: add tag=event_data isTagEnabled=true flags=0x2
11-14 16:00:33.508  1999  2039 W BroadcastQueue: Background execution not allowed: receiving Intent { act=android.intent.action.DROPBOX_ENTRY_ADDED flg=0x10 (has extras) } to com.google.android.gms/.stats.service.DropBoxEntryAddedReceiver
11-14 16:00:33.508  1999  2039 W BroadcastQueue: Background execution not allowed: receiving Intent { act=android.intent.action.DROPBOX_ENTRY_ADDED flg=0x10 (has extras) } to com.google.android.gms/.chimera.GmsIntentOperationService$PersistentTrustedReceiver
11-14 16:00:33.528  2648  4429 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.tapandpay.gcmtask.TapAndPayGcmTaskService started execution. cause:5 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.533  2648  6055 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.checkin.EventLogService finished executing. cause:4 result: 1 elapsed_millis: 104 uptime_millis: 104 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.545  2648  6055 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.tapandpay.gcmtask.TapAndPayGcmTaskService finished executing. cause:5 result: 1 elapsed_millis: 33 uptime_millis: 33 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.567  2648  6055 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.tapandpay.gcmtask.TapAndPayGcmTaskService started execution. cause:5 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.568  2648  6055 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.tapandpay.gcmtask.TapAndPayGcmTaskService finished executing. cause:5 result: 1 elapsed_millis: 10 uptime_millis: 10 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:33.593  1740  1740 D Zygote  : Forked child process 6456
11-14 16:00:33.599  6456  6456 W .android.video: Unexpected CPU variant for X86 using defaults: x86
11-14 16:00:33.610  1999  2040 I ActivityManager: Start proc 6456:com.google.android.videos/u0a129 for service {com.google.android.videos/com.google.android.apps.play.movies.common.service.drm.RefreshLicenseTaskService}
11-14 16:00:33.629  1740  1740 D Zygote  : Forked child process 6473
11-14 16:00:33.635  1999  2040 I ActivityManager: Start proc 6473:com.google.android.youtube/u0a139 for service {com.google.android.youtube/com.google.android.libraries.youtube.common.gcore.gcoreclient.gcm.impl.GcmTaskServiceDelegator}
11-14 16:00:33.636  6473  6473 W android.youtub: Unexpected CPU variant for X86 using defaults: x86
11-14 16:00:33.648  6456  6456 D ApplicationLoaders: Returning zygote-cached class loader: /system/framework/android.hidl.base-V1.0-java.jar
11-14 16:00:33.648  6456  6456 D ApplicationLoaders: Returning zygote-cached class loader: /system/framework/android.hidl.manager-V1.0-java.jar
11-14 16:00:33.648  6456  6456 D ApplicationLoaders: Returning zygote-cached class loader: /system/framework/android.hidl.base-V1.0-java.jar
11-14 16:00:33.650  6456  6456 I .android.video: The ClassLoaderContext is a special shared library.
11-14 16:00:33.655  6456  6456 I .android.video: The ClassLoaderContext is a special shared library.
11-14 16:00:33.686  6473  6473 D ApplicationLoaders: Returning zygote-cached class loader: /system/framework/android.hidl.base-V1.0-java.jar
11-14 16:00:33.686  6473  6473 D ApplicationLoaders: Returning zygote-cached class loader: /system/framework/android.hidl.manager-V1.0-java.jar
11-14 16:00:33.686  6473  6473 D ApplicationLoaders: Returning zygote-cached class loader: /system/framework/android.hidl.base-V1.0-java.jar
11-14 16:00:33.688  6473  6473 I android.youtub: The ClassLoaderContext is a special shared library.
11-14 16:00:33.694  6473  6473 I android.youtub: The ClassLoaderContext is a special shared library.
11-14 16:00:33.741  6473  6515 W YouTube : Fetching the Gservices key 'disable_binder_callback_flush' before the end of the bulk initialization
11-14 16:00:33.758  6473  6521 W YouTube : Fetching the Gservices key 'failsafe_clear_cache_release_13_02' before the end of the bulk initialization
11-14 16:00:33.764  6473  6473 E YouTube : flushBinderConnectionCallbacks is unverified on SDK 29
11-14 16:00:33.779  1999  5718 W ActivityManager: Unable to start service Intent { act=com.android.vending.contentfilters.IContentFiltersService.BIND pkg=com.android.vending } U=0: not found
11-14 16:00:34.349  2648  4429 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.clearcut.uploader.QosUploaderService finished executing. cause:5 result: 1 elapsed_millis: 107 uptime_millis: 107 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:34.536  2648  6055 W Conscrypt: Could not set socket write timeout: java.net.SocketException: Socket closed
11-14 16:00:34.536  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.Platform.setSocketWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):2)
11-14 16:00:34.536  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.ConscryptFileDescriptorSocket.setSoWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):0)
11-14 16:00:34.583  2648  6055 W Conscrypt: Could not set socket write timeout: java.net.SocketException: Socket closed
11-14 16:00:34.583  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.Platform.setSocketWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):2)
11-14 16:00:34.583  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.ConscryptFileDescriptorSocket.setSoWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):0)
11-14 16:00:35.079  2648  6055 W Conscrypt: Could not set socket write timeout: java.net.SocketException: Socket closed
11-14 16:00:35.079  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.Platform.setSocketWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):2)
11-14 16:00:35.079  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.ConscryptFileDescriptorSocket.setSoWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):0)
11-14 16:00:35.130  2648  6055 W Conscrypt: Could not set socket write timeout: java.net.SocketException: Socket closed
11-14 16:00:35.131  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.Platform.setSocketWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):2)
11-14 16:00:35.131  2648  6055 W Conscrypt: 	at com.google.android.gms.org.conscrypt.ConscryptFileDescriptorSocket.setSoWriteTimeout(:com.google.android.gms@202414022@20.24.14 (040700-319035315):0)
11-14 16:00:35.200  2648  3512 I NetworkScheduler.Stats: Task com.google.android.gms/com.google.android.gms.clearcut.uploader.QosUploaderService finished executing. cause:5 result: 1 elapsed_millis: 972 uptime_millis: 972 exec_start_elapsed_seconds: 3805 [CONTEXT service_id=218 ]
11-14 16:00:37.335  1999  2016 D WificondControl: Scan result ready event
11-14 16:00:38.946  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 16:00:38.947  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 16:00:38.947  2998  3244 I MicroDetector: Keeping mic open: false
11-14 16:00:38.947  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 16:00:43.865  6473  6535 W YouTube : Missing task factory for task type: com.google.android.libraries.youtube.offline.task.ScheduledOfflineFlushTask
11-14 16:00:43.949  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 16:00:43.949  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 16:00:43.950  2998  3244 I MicroDetector: Keeping mic open: false
11-14 16:00:43.950  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 16:00:48.954  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 16:00:48.957  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 16:00:48.958  2998  3244 I MicroDetector: Keeping mic open: false
11-14 16:00:48.959  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 16:00:53.961  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 16:00:53.962  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 16:00:53.962  2998  3244 I MicroDetector: Keeping mic open: false
11-14 16:00:53.962  2998  3244 I MicroDetectionWorker: #onError(false)
11-14 16:00:54.415  1999  2038 E memtrack: Couldn't load memtrack module
11-14 16:00:54.415  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 16:00:54.419  1999  2038 E memtrack: Couldn't load memtrack module
11-14 16:00:54.419  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 16:00:54.422  1999  2038 E memtrack: Couldn't load memtrack module
11-14 16:00:54.422  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 16:00:57.424  1999  3015 I ActivityManager: Killing 4121:com.google.android.apps.messaging/u0a130 (adj 999): empty for 3704s
11-14 16:00:57.542  1740  1740 I Zygote  : Process 4121 exited due to signal 9 (Killed)
11-14 16:00:57.554  1999  2041 I libprocessgroup: Successfully killed process cgroup uid 10130 pid 4121 in 127ms
11-14 16:00:58.964  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]
11-14 16:00:58.965  2998  3244 W ErrorReporter: reportError [type: 211, code: 393244, bug: 0]: errorCode: 393244, engine: 0
11-14 16:01:14.476  1999  2038 E memtrack: Couldn't load memtrack module
11-14 16:01:14.476  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 16:01:14.480  1999  2038 E memtrack: Couldn't load memtrack module
11-14 16:01:14.480  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 16:01:14.482  1999  2038 E memtrack: Couldn't load memtrack module
11-14 16:01:14.482  1999  2038 W android.os.Debug: failed to get memory consumption info: -1
11-14 16:01:18.052  1889  1889 E netmgr  : Failed to open QEMU pipe 'qemud:network': Invalid argument
11-14 16:01:43.998  2998  3244 I MicroDetectionWorker: #startMicroDetector [speakerMode: 0]";

    public readonly static List<string> FAKE_DEVICES = new() { "Emulator-8884", "Pixel-4a-5G", "Emulator-88834" };
}