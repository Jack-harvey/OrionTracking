import registerComponent from "../../../core/component_registrator";
import BaseComponent from "../../component_wrapper/common/component";
import { Scheduler as SchedulerComponent } from "./scheduler";
export default class Scheduler extends BaseComponent {
  getProps() {
    var props = super.getProps();
    props.onKeyDown = this._wrapKeyDownHandler(props.onKeyDown);
    return props;
  }

  getComponentInstance() {
    var _this$viewRef;

    return (_this$viewRef = this.viewRef) === null || _this$viewRef === void 0 ? void 0 : _this$viewRef.getComponentInstance(...arguments);
  }

  addAppointment(appointment) {
    var _this$viewRef2;

    return (_this$viewRef2 = this.viewRef) === null || _this$viewRef2 === void 0 ? void 0 : _this$viewRef2.addAppointment(...arguments);
  }

  deleteAppointment(appointment) {
    var _this$viewRef3;

    return (_this$viewRef3 = this.viewRef) === null || _this$viewRef3 === void 0 ? void 0 : _this$viewRef3.deleteAppointment(...arguments);
  }

  updateAppointment(target, appointment) {
    var _this$viewRef4;

    return (_this$viewRef4 = this.viewRef) === null || _this$viewRef4 === void 0 ? void 0 : _this$viewRef4.updateAppointment(...arguments);
  }

  getDataSource() {
    var _this$viewRef5;

    return (_this$viewRef5 = this.viewRef) === null || _this$viewRef5 === void 0 ? void 0 : _this$viewRef5.getDataSource(...arguments);
  }

  getEndViewDate() {
    var _this$viewRef6;

    return (_this$viewRef6 = this.viewRef) === null || _this$viewRef6 === void 0 ? void 0 : _this$viewRef6.getEndViewDate(...arguments);
  }

  getStartViewDate() {
    var _this$viewRef7;

    return (_this$viewRef7 = this.viewRef) === null || _this$viewRef7 === void 0 ? void 0 : _this$viewRef7.getStartViewDate(...arguments);
  }

  hideAppointmentPopup(saveChanges) {
    var _this$viewRef8;

    return (_this$viewRef8 = this.viewRef) === null || _this$viewRef8 === void 0 ? void 0 : _this$viewRef8.hideAppointmentPopup(...arguments);
  }

  hideAppointmentTooltip() {
    var _this$viewRef9;

    return (_this$viewRef9 = this.viewRef) === null || _this$viewRef9 === void 0 ? void 0 : _this$viewRef9.hideAppointmentTooltip(...arguments);
  }

  scrollTo(date, group, allDay) {
    var _this$viewRef10;

    return (_this$viewRef10 = this.viewRef) === null || _this$viewRef10 === void 0 ? void 0 : _this$viewRef10.scrollTo(...arguments);
  }

  scrollToTime(hours, minutes, date) {
    var _this$viewRef11;

    return (_this$viewRef11 = this.viewRef) === null || _this$viewRef11 === void 0 ? void 0 : _this$viewRef11.scrollToTime(...arguments);
  }

  showAppointmentPopup(appointmentData, createNewAppointment, currentAppointmentData) {
    var _this$viewRef12;

    return (_this$viewRef12 = this.viewRef) === null || _this$viewRef12 === void 0 ? void 0 : _this$viewRef12.showAppointmentPopup(...arguments);
  }

  showAppointmentTooltip(appointmentData, target, currentAppointmentData) {
    var _this$viewRef13;

    var params = [appointmentData, this._patchElementParam(target), currentAppointmentData];
    return (_this$viewRef13 = this.viewRef) === null || _this$viewRef13 === void 0 ? void 0 : _this$viewRef13.showAppointmentTooltip(...params.slice(0, arguments.length));
  }

  _getActionConfigs() {
    return {
      onAppointmentAdded: {},
      onAppointmentAdding: {},
      onAppointmentClick: {},
      onAppointmentContextMenu: {},
      onAppointmentDblClick: {},
      onAppointmentDeleted: {},
      onAppointmentDeleting: {},
      onAppointmentFormOpening: {},
      onAppointmentRendered: {},
      onAppointmentUpdated: {},
      onAppointmentUpdating: {},
      onCellClick: {},
      onCellContextMenu: {},
      onClick: {}
    };
  }

  get _propsInfo() {
    return {
      twoWay: [["currentDate", "defaultCurrentDate", "currentDateChange"], ["currentView", "defaultCurrentView", "currentViewChange"]],
      allowNull: [],
      elements: [],
      templates: ["dataCellTemplate", "dateCellTemplate", "timeCellTemplate", "resourceCellTemplate", "appointmentCollectorTemplate", "appointmentTemplate", "appointmentTooltipTemplate"],
      props: ["adaptivityEnabled", "appointmentDragging", "crossScrollingEnabled", "dataSource", "dateSerializationFormat", "descriptionExpr", "editing", "focusStateEnabled", "groupByDate", "indicatorUpdateInterval", "max", "min", "noDataText", "recurrenceEditMode", "remoteFiltering", "resources", "scrolling", "selectedCellData", "shadeUntilCurrentTime", "showAllDayPanel", "showCurrentTimeIndicator", "timeZone", "useDropDownViewSwitcher", "views", "endDayHour", "startDayHour", "firstDayOfWeek", "cellDuration", "groups", "maxAppointmentsPerCell", "customizeDateNavigatorText", "onAppointmentAdded", "onAppointmentAdding", "onAppointmentClick", "onAppointmentContextMenu", "onAppointmentDblClick", "onAppointmentDeleted", "onAppointmentDeleting", "onAppointmentFormOpening", "onAppointmentRendered", "onAppointmentUpdated", "onAppointmentUpdating", "onCellClick", "onCellContextMenu", "recurrenceExceptionExpr", "recurrenceRuleExpr", "startDateExpr", "startDateTimeZoneExpr", "endDateExpr", "endDateTimeZoneExpr", "allDayExpr", "textExpr", "dataCellTemplate", "dateCellTemplate", "timeCellTemplate", "resourceCellTemplate", "appointmentCollectorTemplate", "appointmentTemplate", "appointmentTooltipTemplate", "toolbar", "defaultCurrentDate", "currentDateChange", "defaultCurrentView", "currentViewChange", "className", "accessKey", "activeStateEnabled", "disabled", "height", "hint", "hoverStateEnabled", "onClick", "onKeyDown", "rtlEnabled", "tabIndex", "visible", "width", "currentDate", "currentView"]
    };
  }

  get _viewComponent() {
    return SchedulerComponent;
  }

}
registerComponent("dxScheduler", Scheduler);