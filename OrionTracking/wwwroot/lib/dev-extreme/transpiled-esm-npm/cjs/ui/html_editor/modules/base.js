"use strict";

exports.default = void 0;

var _devextremeQuill = _interopRequireDefault(require("devextreme-quill"));

var _empty = _interopRequireDefault(require("./empty"));

var _type = require("../../../core/utils/type");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _slicedToArray(arr, i) { return _arrayWithHoles(arr) || _iterableToArrayLimit(arr, i) || _unsupportedIterableToArray(arr, i) || _nonIterableRest(); }

function _nonIterableRest() { throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); }

function _unsupportedIterableToArray(o, minLen) { if (!o) return; if (typeof o === "string") return _arrayLikeToArray(o, minLen); var n = Object.prototype.toString.call(o).slice(8, -1); if (n === "Object" && o.constructor) n = o.constructor.name; if (n === "Map" || n === "Set") return Array.from(o); if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen); }

function _arrayLikeToArray(arr, len) { if (len == null || len > arr.length) len = arr.length; for (var i = 0, arr2 = new Array(len); i < len; i++) { arr2[i] = arr[i]; } return arr2; }

function _iterableToArrayLimit(arr, i) { var _i = arr == null ? null : typeof Symbol !== "undefined" && arr[Symbol.iterator] || arr["@@iterator"]; if (_i == null) return; var _arr = []; var _n = true; var _d = false; var _s, _e; try { for (_i = _i.call(arr); !(_n = (_s = _i.next()).done); _n = true) { _arr.push(_s.value); if (i && _arr.length === i) break; } } catch (err) { _d = true; _e = err; } finally { try { if (!_n && _i["return"] != null) _i["return"](); } finally { if (_d) throw _e; } } return _arr; }

function _arrayWithHoles(arr) { if (Array.isArray(arr)) return arr; }

function _inheritsLoose(subClass, superClass) { subClass.prototype = Object.create(superClass.prototype); subClass.prototype.constructor = subClass; _setPrototypeOf(subClass, superClass); }

function _setPrototypeOf(o, p) { _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) { o.__proto__ = p; return o; }; return _setPrototypeOf(o, p); }

var BaseModule = _empty.default;

if (_devextremeQuill.default) {
  var BaseQuillModule = _devextremeQuill.default.import('core/module');

  BaseModule = /*#__PURE__*/function (_BaseQuillModule) {
    _inheritsLoose(BaseHtmlEditorModule, _BaseQuillModule);

    function BaseHtmlEditorModule(quill, options) {
      var _this;

      _this = _BaseQuillModule.call(this, quill, options) || this;
      _this.editorInstance = options.editorInstance;
      return _this;
    }

    var _proto = BaseHtmlEditorModule.prototype;

    _proto.saveValueChangeEvent = function saveValueChangeEvent(event) {
      this.editorInstance._saveValueChangeEvent(event);
    };

    _proto.addCleanCallback = function addCleanCallback(callback) {
      this.editorInstance.addCleanCallback(callback);
    };

    _proto.handleOptionChangeValue = function handleOptionChangeValue(changes) {
      var _this2 = this;

      if ((0, _type.isObject)(changes)) {
        Object.entries(changes).forEach(function (_ref) {
          var _ref2 = _slicedToArray(_ref, 2),
              name = _ref2[0],
              value = _ref2[1];

          return _this2.option(name, value);
        });
      } else if (!(0, _type.isDefined)(changes)) {
        this === null || this === void 0 ? void 0 : this.clean();
      }
    };

    return BaseHtmlEditorModule;
  }(BaseQuillModule);
}

var _default = BaseModule;
exports.default = _default;
module.exports = exports.default;
module.exports.default = exports.default;